using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IEntryDal entryDal) : ControllerBase
    {
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


        //private string CreateJwt(User user)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("veryverysceret.....");
        //    var identity = new ClaimsIdentity(new Claim[]
        //    {
        //        new Claim(ClaimTypes.Role, "user"),
        //        new Claim(ClaimTypes.Name,$"{user.Login}")
        //    });

        //    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = identity,
        //        Expires = DateTime.Now.AddSeconds(10),
        //        SigningCredentials = credentials
        //    };
        //    var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //    return jwtTokenHandler.WriteToken(token);
        //}
    }
}
