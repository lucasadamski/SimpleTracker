using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using SimpleTracker.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserDal userDal, Serilog.ILogger logger) : ControllerBase
    {
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User user)
        {
            if (user == null)
            {
                logger.Error("User is null.");
                return BadRequest();
            }
                
            var existingUser = userDal.ReadUserByToken(user.Token); //read user 

            if (existingUser == null)
            {
                logger.Warning("User token is not in DB. Returning Login view.");
                return Login(user);
            }

            return Ok();
        }


        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            if (user == null)
            {
                logger.Error("User is null.");
                return BadRequest();
            }

            logger.Debug("User creds: {login} {password}", user.Login, user.Password);

            var existingUser = userDal.ReadUser(user.Login, user.Password);

            if (existingUser == null)
                return NotFound(new { Message = "Incorrect login or password" });

            existingUser.Token = CreateToken(user);
            userDal.Update(existingUser);       

            return Ok(new               
            {
                existingUser.Token
            });
        }

        private bool DoesLoginAlreadyExists(string login)
        {
            if(string.IsNullOrWhiteSpace(login)) return false;
            var user = userDal.ReadUser(login);
            if (user != null && !string.IsNullOrEmpty(user.Login))
                return true;
            return false;
        }

        [HttpPost("Create")]
        public IActionResult Create(User user)
        {
            if (user == null)
                return BadRequest();

            logger.Debug("Creating User creds: {login} {password}", user.Login, user.Password);

            if (DoesLoginAlreadyExists(user.Login)) 
                return BadRequest();

            var token = CreateToken(user);
            var newUser = new User()
            {
                Login = user.Login,
                Password = user.Password,
                Token = token
            };
            userDal.CreateNewUser(newUser);

            return Ok(new { token });
        }

        private string CreateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler(); 
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name,$"{user.Login}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(TokenKey.Key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
