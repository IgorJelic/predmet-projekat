using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Dto;
using UsersService.Services.Interfaces;

namespace UsersService.Services
{
    public class UserService : IUserService
    {
        public UserRegisterDto ActivateUser(long userId)
        {
            throw new NotImplementedException();
        }

        public List<DelivererInfoDto> GetDeliverers()
        {
            throw new NotImplementedException();
        }

        public UserRegisterDto GetUser(long userId)
        {
            throw new NotImplementedException();
        }

        public string Login(UserLoginDto loginInfo)
        {
            throw new NotImplementedException();
        }

        public UserRegisterDto Register(UserRegisterDto registerInfo)
        {
            throw new NotImplementedException();
        }

        public string UpdateProfilePhoto(long userId, IFormFile newImage)
        {
            throw new NotImplementedException();
        }

        public UserRegisterDto UpdateUser(long userId, UserRegisterDto updateInfo)
        {
            throw new NotImplementedException();
        }

        public string UploadImage(IFormFile image)
        {
            throw new NotImplementedException();
        }
    }
}
