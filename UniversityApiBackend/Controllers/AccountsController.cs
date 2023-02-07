using InternationalAPI.Resources.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDBContext _context;
        private readonly IStringLocalizer<AccountsController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

        public AccountsController(JwtSettings jwtSettings, 
                                  UniversityDBContext universityDBContext, 
                                  IStringLocalizer<AccountsController> stringLocalizer, 
                                  IStringLocalizer<SharedResource> sharedResourceLocalizer)
        {
            _jwtSettings = jwtSettings;
            _context = universityDBContext;
            _stringLocalizer = stringLocalizer;
            _sharedResourceLocalizer = sharedResourceLocalizer;
        }

        //private IEnumerable<User> Logins = new List<User>()
        //{
        //    new User()
        //    {
        //        Id= 1,
        //        EmailAddress="ts@dominio.com",
        //        Name = "Admin",
        //        Password = "Admin"
        //    },
        //    new User()
        //    {
        //        Id= 2,
        //        EmailAddress="pepe@dominio.com",
        //        Name = "User1",
        //        Password = "pepe"
        //    }
        //};

        [HttpPost("GetToken")]
        public IActionResult GetToken(UserLogins userLogin) 
        {
            try
            {
                var Token = new UserTokens();
                // Search a user in context with LINQ
                var searchUser = (from user in _context.Users
                                 where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                 select user).FirstOrDefault();

                //var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                if(searchUser != null)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName= searchUser.Name,
                        EmailId= searchUser.EmailAddress,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                var postName = _stringLocalizer.GetString("Welcome").Value ?? string.Empty;

                return Ok(new
                {
                    PostName = postName,
                    TokenGenerated = Token,
                });


            }
            catch(Exception ex) 
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
        public IActionResult GetUserList()
        {
            return Ok((from user in _context.Users
                       select user).ToList());
        }
    }
}
