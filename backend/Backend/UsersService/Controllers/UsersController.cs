using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersService.Dto;
using UsersService.Services.Interfaces;

namespace UsersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // LOGIN / REGISTER
        // POST api/Users/Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginDto userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = _userService.Login(userLogin);

            if (token == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            return Ok(token);
        }

        // POST api/Users/Register
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterDto userRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _userService.Register(userRegister);
                return Ok(user);
            }
            catch (Exception)
            {
                return base.Conflict($"Product name [{userRegister.Email}] is already taken!");
            }
        }


        // USER PROFILE
        // GET api/Users/User
        [HttpGet]
        [Route("User")]
        //[Authorize(Roles="logged")]
        public IActionResult GetUser()
        {
            long userId = -1;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return BadRequest();
            }

            var userInfo = _userService.GetUser(userId);
            if (userInfo == null)
            {
                return NotFound();
            }

            return Ok(userInfo);
        }

        // PUT api/Users/EditProfile
        //[Authorize(Roles="logged")]
        [HttpPut]
        [Route("User")]
        public IActionResult EditProfile(UserRegisterDto updateInfo)
        {
            long userId = -1;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                return BadRequest();
            }

            try
            {
                var updatedUser = _userService.UpdateUser(userId, updateInfo);
                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch (Exception)
            {
                return base.Conflict($"Product name [{updateInfo.Email}] is already taken!");
            }            
        }


        // DELIVERERS
        // GET api/Users/MyDeliverers
        [HttpGet]
        [Route("MyDeliverers")]
        //[Authorize(Roles="administrator")]
        public IActionResult Deliverers()
        {
            return Ok(_userService.GetDeliverers());
        }


        // ACTIVATE / REJECT DELIVERER
        // GET api/Users/ActivateProfile/{delivererId}
        [HttpGet]
        [Route("ActivateProfile/{delivererId}")]
        //[Authorize(Roles="administrator")]
        public IActionResult ActivateProfile(long delivererId)
        {
            var activatedUser = _userService.ActivateUser(delivererId);

            if (activatedUser == null)
            {
                return NotFound();
            }

            return Ok(activatedUser);
        }

        // GET api/Users/BlockProfile/{delivererId}
        [HttpGet]
        [Route("BlockProfile/{delivererId}")]
        //[Authorize(Roles="administrator")]
        public IActionResult BlockProfile(long delivererId)
        {
            var rejectedUser = _userService.RejectUser(delivererId);

            if (rejectedUser == null)
            {
                return NotFound();
            }

            return Ok(rejectedUser);
        }


        // IMAGES
        // POST api/Users/UploadImage
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage()
        {
            var file = Request.Form.Files[0];

            long userId = -1;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                // or
                userId = long.Parse(identity.FindFirst("id").Value);

                var path = _userService.UpdateProfilePhoto(userId, file);
                if (path == "")
                {
                    return StatusCode(500);
                }

                //var obj = new
                //{
                //    slikaPath = path
                //};

                return Ok(new { slikaPath = path });
            }
            else
            {
                var path = _userService.UploadImage(file);
                if (path == "")
                {
                    return StatusCode(500);
                }

                return Ok(new { slikaPath = path });
            }
        }

    }
}
