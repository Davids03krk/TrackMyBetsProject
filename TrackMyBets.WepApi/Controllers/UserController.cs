using System;
using Microsoft.AspNetCore.Mvc;
using TrackMyBets.Data.Models;
using TrackMyBets.WepApi.Helpers;
using Microsoft.Extensions.Options;
using TrackMyBets.Model;
using TrackMyBets.Business.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TrackMyBets.WepApi.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private static BD_TRACKMYBETSContext _dbContext;
        private readonly AppSettings _appSettings;

        public UserController(
            IOptions<AppSettings> appSettings,
            BD_TRACKMYBETSContext dbContext) {

            _appSettings = appSettings.Value;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin userLogin) {

            var userAuth = UserEntity.Authenticate(userLogin.Nick, userLogin.Password);

            if (userAuth == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAuth.IdUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new UserAuth {
                IdUser = userAuth.IdUser,
                Nick = userAuth.Nick,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel userModel) {
            try {
                UserEntity.Create(userModel.ToEntities());
                return Ok();
            } catch (Exception ex) {
                return BadRequest("ERROR: [User Register] [" + ex.Message + "]");
            }
        }

        [HttpGet("{idUser}")]
        public IActionResult GetById(int idUser) {
            try {
                UserModel userModel = UserModel.FromEntity(UserEntity.Load(idUser));

                if (userModel != null)
                    return Ok(userModel);
                else
                    return BadRequest("WARN: [The user is null]");
            }
            catch (Exception ex) {
                return BadRequest("ERROR: [User By Id] [" + ex.Message + "]");
            }
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody] UserModel userModel) {
            try {
                userModel.ToEntities().Update();
                return Ok();
            } catch (Exception ex) {
                return BadRequest("ERROR: [User Update] [" + ex.Message + "]");
            }
        }

        [HttpDelete("{idUser}")]
        public IActionResult Delete(int idUser) {
            try {
                UserEntity.Load(idUser).Delete();
                return Ok();
            } catch (Exception ex) {
                return BadRequest("ERROR: [User Delete] [" + ex.Message + "]");
            }
        }
    }
}