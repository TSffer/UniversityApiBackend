using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using universityApiBackend.Helpers;
using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings)
        {
            this._jwtSettings = jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                EmailAddress = "exmaple@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id = 2,
                EmailAddress = "pepe@gmail.com",
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
                        UserName = user.Name,
                        EmailId = user.EmailAddress,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
