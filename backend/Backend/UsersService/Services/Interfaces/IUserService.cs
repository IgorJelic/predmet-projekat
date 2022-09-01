using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Dto;

namespace UsersService.Services.Interfaces
{
    public interface IUserService
    {
        string Login(UserLoginDto loginInfo);
        UserRegisterDto Register(UserRegisterDto registerInfo);
        UserRegisterDto GetUser(long userId);
        UserRegisterDto UpdateUser(long userId, UserRegisterDto updateInfo);
        UserRegisterDto ActivateUser(long userId);
        UserRegisterDto RejectUser(long userId);
        List<DelivererInfoDto> GetDeliverers();
        string UploadImage(IFormFile image);
        string UpdateProfilePhoto(long userId, IFormFile newImage);
    }
}
