using GearShiftAPI.Data;
using GearShiftAPI.JWTFeatures;
using GearShiftAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;

namespace GearShiftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly JWTHandler _jwtHandler;

        public LoginController(AppDBContext appDBContext, JWTHandler jwtHandler)
        {
            _context = appDBContext;
            _jwtHandler = jwtHandler;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.userModel.AsQueryable();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUserProfile(int id)
        {
            var user = _context.userModel.AsNoTracking().FirstOrDefault(x => x.Id == id);
            var userProfile = new
            {
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Streeet_address = user.Street_address,
                City = user.City,
                State = user.State,
                Zipcode = user.Zipcode,
                Phone = user.Phone
            };

            return Ok(userProfile);
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] UserModel userObj)
        {
            if(userObj == null)
            {
                return BadRequest();
            } 
            else
            {
                _context.userModel.Add(userObj);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "User added successfully"
                });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginObj)
        {
            if(loginObj == null)
            {
                return BadRequest();
            } 
            else
            {
                var user = _context.userModel.Where(a =>
                a.Email == loginObj.Email && a.Password == loginObj.Password).FirstOrDefault();

                if(user != null)
                {
                    var signingCredentials = _jwtHandler.GetSigningCredentials();
                    var claims = _jwtHandler.GetClaims(user);
                    var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(new AuthResponse
                    {
                        IsAuthSuccessful = true,
                        Token = token
                    });
                }
                else
                {
                    return Unauthorized(new AuthResponse { ErrorMessage = "Invalid authentication"});
                }
            }
        }
    }
}
