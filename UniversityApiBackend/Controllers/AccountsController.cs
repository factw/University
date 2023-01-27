﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountsController(JwtSettings jwtSettings)
        {
            _jwtSettings= jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id= 1,
                EmailAddress="ts@dominio.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id= 2,
                EmailAddress="pepe@dominio.com",
                Name = "User1",
                Password = "pepe"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin) 
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if(Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName= user.Name,
                        EmailId= user.EmailAddress,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);

                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);


            }catch(Exception ex) 
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public IActionResult GetUserList()
        { 
            return Ok(Logins);
        }
    }
}
