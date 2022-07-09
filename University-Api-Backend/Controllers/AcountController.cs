using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Api_Backend.Helpers;
using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AcountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }

        private IEnumerable<Usuario> Logins = new List<Usuario>()
        {
            new Usuario()
            {
                Id = 1,
                Email = "eere@rieei.com",
                Name = "Admin",
                Password = "Admin"
            },
             new Usuario()
            {
                Id = 2,
                Email = "pepe@erere.com",
                Name = "User1",
                Password = "pepe"
            },
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any(usuario => usuario.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                
                if (Valid)
                {
                    var usuario = Logins.FirstOrDefault(usuario => usuario.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = usuario.Name, 
                        EmailId = usuario.Email,
                        Id = usuario.Id,
                        GuidID = Guid.NewGuid(),

                    }, _jwtSettings);
                
                }
                else
                {
                    return BadRequest("Wrong password");
                }

                return Ok(Token);
            
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error",ex);

            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")]
        public IActionResult GetUsersList()
        {
            return Ok(Logins);
        }


    }
}
