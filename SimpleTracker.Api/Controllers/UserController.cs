using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserDal userDal) : ControllerBase
    {
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User incomingUser)
        {
            if (incomingUser == null)
                return BadRequest();

            var user = userDal.ReadUser(incomingUser.Login, incomingUser.Password); //read user 

            if (user == null)
                return NotFound(new { Message = "Incorrect login or password" });

            user.Token = CreateJwt(user); // create token
            var newAccessToken = user.Token;
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryDate = DateTime.Now.AddDays(5);

            userDal.Update(user);       // update user

            return Ok(new               // return token
            {
                newAccessToken,
                newRefreshToken
            });
        }


        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            var userId = 1;
            if (user.Login == "a" && user.Password == "a")
                return StatusCode(200, userId);

            return StatusCode(500, 0);
        }




        //[HttpPost("authenticate")]
        //public async IActionResult Authenticate([FromBody] User userObj)
        //{
        //    if (userObj == null)
        //        return BadRequest();

        //    var user = new User();

        //    if (user == null)
        //        return NotFound(new { Message = "User not found!" });

           

        //    user.Token = CreateJwt(user);
        //    var newAccessToken = user.Token;
        //    var newRefreshToken = CreateRefreshToken();
        //    user.RefreshToken = newRefreshToken;
        //    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
        //    await _authContext.SaveChangesAsync();

        //    return Ok(new TokenApiDto()
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefreshToken
        //    });
        //}


        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name,$"{user.Login}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(10),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;

        }
    }
}
