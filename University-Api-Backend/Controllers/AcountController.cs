using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using University_Api_Backend.DataAccess;
using University_Api_Backend.Helpers;
using University_Api_Backend.Models.DataModels;
using System.Linq;
using University_Api_Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace University_Api_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AcountController : ControllerBase
    {


        private readonly JwtSettings _jwtSettings;
        
        private readonly UniversityDBContext _context;

        public AcountController(JwtSettings jwtSettings, UniversityDBContext context)
        {
            _jwtSettings = jwtSettings;

            
            _context = context;
        }

        // Example users
        // TODO: Change by real users in DB

        private IEnumerable<Usuario> Logins = new List<Usuario>()
        {
            new Usuario()
            {
                Id = 1,
                Email ="Erick@rieei.com",
                Name = "Admin",
                Password = "Admin"
            },
             new Usuario()
            {
                Id = 2,
                Email = "pepe@erere.com",
                Name = "User 1",
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
                throw new Exception("GetToken Error", ex);

            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUsersList()
        {
            return Ok(Logins);
        }


        [HttpGet("{nombre}, {password}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "User")]
        public IActionResult GetUsuario(string nombre,string password)
        {
            try
            {
                if (_context.Usuarios == null)
                {
                    return NotFound();
                }
                var usuario = _context.Usuarios.ToList();

                var resultado = from user in usuario
                                where user.Name == nombre && user.Password == password
                                select user;


                if (resultado == null)
                {
                    return NotFound();
                }

                


                Usuario resul = resultado.First();

                return Ok(resul);


                
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
