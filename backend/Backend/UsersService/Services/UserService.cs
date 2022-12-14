using AutoMapper;
using EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersService.DbInfrastructure;
using UsersService.Dto;
using UsersService.Models;
using UsersService.Services.Interfaces;

namespace UsersService.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IConfigurationSection _secretKey;

        public UserService(IConfiguration configuration, IMapper mapper, UserDbContext dbContext, IEmailSender emailSender)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _emailSender = emailSender;
            _secretKey = configuration.GetSection("SecretKey");
        }

        // LOGIN / REGISTER
        public string Login(UserLoginDto loginInfo)
        {
            User user = _dbContext.Users.FirstOrDefault(u => u.Email == loginInfo.Email);

            if (user == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(loginInfo.Password, user.Password))
            {
                List<Claim> userClaims = new List<Claim>();

                userClaims.Add(new Claim("id", user.Id.ToString()));
                userClaims.Add(new Claim("status", user.ProfileStatus));
                userClaims.Add(new Claim("name", $"{user.FirstName} {user.LastName}"));
                userClaims.Add(new Claim("img", user.ImagePath));
                // logged?
                userClaims.Add(new Claim("logged", "true"));

                if (user.Role == "administrator")
                {
                    //userClaims.Add(new Claim(ClaimTypes.Role, "administrator"));
                    userClaims.Add(new Claim("role", "administrator"));
                    userClaims.Add(new Claim("userrole", "administrator"));
                }
                if (user.Role == "customer")
                {
                    //userClaims.Add(new Claim(ClaimTypes.Role, "customer"));
                    userClaims.Add(new Claim("role", "customer"));
                    userClaims.Add(new Claim("userrole", "customer"));
                    userClaims.Add(new Claim("orders", "true"));
                }
                if (user.Role == "deliverer")
                {
                    //userClaims.Add(new Claim(ClaimTypes.Role, "deliverer"));
                    userClaims.Add(new Claim("role", "deliverer"));
                    userClaims.Add(new Claim("userrole", "deliverer"));
                    userClaims.Add(new Claim("orders", "true"));
                }

                //Kreiramo kredencijale za potpisivanje tokena. Token mora biti potpisan privatnim kljucem
                //kako bi se sprecile njegove neovlascene izmene
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44350/", //url servera koji je izdao token
                    claims: userClaims, //claimovi
                    expires: DateTime.Now.AddMinutes(20), //vazenje tokena u minutama
                    signingCredentials: signinCredentials //kredencijali za potpis
                );

                string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return tokenString;
            }
            else
            {
                return null;
            }
        }

        public UserRegisterDto Register(UserRegisterDto registerInfo)
        {
            try
            {
                User newUser = _mapper.Map<User>(registerInfo);

                newUser.Password = BCrypt.Net.BCrypt.HashPassword(registerInfo.Password);

                //newUser.CustomerOrders = new List<Order>();
                //newUser.DelivererOrders = new List<Order>();

                if (newUser.Role == "deliverer")
                {
                    newUser.ProfileStatus = "pending";
                    newUser.DelivererOrders = new List<Order>();
                }
                else
                {
                    newUser.ProfileStatus = "active";
                    newUser.CustomerOrders = new List<Order>();
                }

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                return _mapper.Map<UserRegisterDto>(newUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // AKTIVACIJA / ODBIJANJE DOSTAVLJACA
        public UserRegisterDto ActivateUser(long userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            user.ProfileStatus = "active";
            _dbContext.SaveChanges();

            // Email service
            var emailAddress = user.Email;
            var message = new Message(new string[] { $"{emailAddress}" }, "Aktivacija profila", "Dobrodosli, Vas profil je AKTIVIRAN! DostavaApp by Igor Jelic");
            _emailSender.SendEmail(message);

            return _mapper.Map<UserRegisterDto>(user);
        }

        public UserRegisterDto RejectUser(long userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            user.ProfileStatus = "rejected";
            _dbContext.SaveChanges();

            // Email service
            var emailAddress = user.Email;
            var message = new Message(new string[] { $"{emailAddress}" }, "Aktivacija profila", "Nazalost, Vas profil je ODBIJEN! DostavaApp by Igor Jelic");
            _emailSender.SendEmail(message);

            return _mapper.Map<UserRegisterDto>(user);
        }


        // USERS / DELIVERERS
        public List<DelivererInfoDto> GetDeliverers()
        {
            List<DelivererInfoDto> delivererList = _mapper.Map<List<DelivererInfoDto>>(_dbContext.Users.Where(u => u.Role == "deliverer"));

            return delivererList;
        }

        public UserRegisterDto GetUser(long userId)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserRegisterDto>(user);
        }

        public UserRegisterDto UpdateUser(long userId, UserRegisterDto updateInfo)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return null;
            }
            else
            {
                try
                {
                    user.FirstName = updateInfo.FirstName;
                    user.LastName = updateInfo.LastName;
                    user.Email = updateInfo.Email;
                    user.DateOfBirth = updateInfo.DateOfBirth;

                    _dbContext.SaveChanges();
                    return _mapper.Map<UserRegisterDto>(user);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }



        // IMAGE WORK
        public string UploadImage(IFormFile image)
        {
            if (image != null)
            {
                if (image.Length > 0)
                {
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    var fileName = ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    return fileName;
                }
            }

            return "";
        }
        
        public string UpdateProfilePhoto(long userId, IFormFile newImage)
        {
            var user = _dbContext.Users.Find(userId);

            if (user == null)
            {
                return "";
            }
            else
            {
                if (newImage != null)
                {
                    if (newImage.Length > 0)
                    {
                        var folderName = Path.Combine("Resources", "Images");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        var fileName = ContentDispositionHeaderValue.Parse(newImage.ContentDisposition).FileName.Trim('"');
                        var fullPath = Path.Combine(pathToSave, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            newImage.CopyTo(stream);
                        }

                        user.ImagePath = fileName;
                        _dbContext.SaveChanges();

                        return fileName;
                    }
                }

                return "";
            }
        }
    }
}
