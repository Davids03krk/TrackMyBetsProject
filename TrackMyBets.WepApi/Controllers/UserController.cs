using System;
using Microsoft.AspNetCore.Mvc;
using TrackMyBets.Model;
using TrackMyBets.Business.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TrackMyBets.Business.Configurations;

namespace TrackMyBets.WepApi.Controllers
{
    [Produces("application/json")]
    [Route("User")]
    public class UserController : Controller
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin) {

            var userAuth = UserEntity.Authenticate(userLogin.Nick, userLogin.Password);

            if (userAuth == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.SecretAuth);
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
        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserModel userModel) {
            try {
                UserEntity.Create(userModel.ToEntities());
                return Ok();
            } catch (Exception ex) {
                return BadRequest("ERROR: [User Register] [" + ex.Message + "]");
            }
        }

        [HttpGet("GetById/{idUser}")]
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

        [HttpPost("Update")]
        public IActionResult Update([FromBody] UserModel userModel) {
            try {
                userModel.ToEntities().Update();
                return Ok();
            } catch (Exception ex) {
                return BadRequest("ERROR: [User Update] [" + ex.Message + "]");
            }
        }

        [HttpDelete("Delete/{idUser}")]
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