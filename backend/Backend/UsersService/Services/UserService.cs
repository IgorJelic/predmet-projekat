using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.DbInfrastructure;
using UsersService.Dto;
using UsersService.Services.Interfaces;

namespace UsersService.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;

        public UserService(IConfiguration configuration, IMapper mapper, UserDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = configuration.GetSection("SecretKey");
        }

        // LOGIN / REGISTER
        public string Login(UserLoginDto loginInfo)
        {
            throw new NotImplementedException();
        }

        public UserRegisterDto Register(UserRegisterDto registerInfo)
        {
            throw new NotImplementedException();
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
                user.FirstName = updateInfo.FirstName;
                user.LastName = updateInfo.LastName;
                user.Email = updateInfo.Email;
                user.DateOfBirth = updateInfo.DateOfBirth;

                _dbContext.SaveChanges();
                return _mapper.Map<UserRegisterDto>(user);
            }
        }



        // IMAGE WORK
        public string UpdateProfilePhoto(long userId, IFormFile newImage)
        {
            throw new NotImplementedException();
        }

        public string UploadImage(IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}
