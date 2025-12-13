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
        public IActionResult Authenticate([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            var existingUser = userDal.ReadUser(user.Login, user.Password); //read user 

           
           return NotFound(new { Message = "Incorrect login or password" }); // to do not finished

            
        }


        [HttpPost("Login")]
        public IActionResult Login(User user)
        {
            if (user == null)
                return BadRequest();

            var existingUser = userDal.ReadUser(user.Login, user.Password); //read user 

            if (user == null || existingUser.Login != user.Login || existingUser.Password != user.Password)
                return NotFound(new { Message = "Incorrect login or password" });

            existingUser.Token = CreateJwt(user); // create token
            userDal.Update(existingUser);       // update user

            return Ok(new               // return token
            {
                newAccessToken = existingUser.Token
            });
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
