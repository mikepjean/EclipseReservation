using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EclipseLink.UserManagement;

namespace EclipseLink
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUserStore _userStore;
        private readonly IConfiguration _configuration;

        public UserRegistrationController(IUserStore userStore, IConfiguration configuration)
        {
            _userStore = userStore;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate user credentials
            var user = _userStore.GetUserByUserName(request.Username);
            if (user is not null && user.Username == request.Username) // Fix for CS0019 and CS1061
            {
                // Generate JWT token
                var token = GenerateJwtToken(user.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials.");
        }

        // DTO for login request
        public class LoginRequest
        {
            public int UserId { get; set; }
            public string Username { get; set; }
        }



        // Endpoint to create a new user and return a JWT token
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] EclipseLink.UserManagement.RegisterUserRequest user)
        {
            if(_userStore == null)
            {
                return BadRequest("User store is not initialized.");
            }
            if (_userStore.GetUserByUserName(user.Username) != null)
            {
                return BadRequest("User already exists.");
            }
            var _user = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                ReceiveNotifications = user.ReceiveNotifications
            };
            _userStore.Save(_user);
            var token = GenerateJwtToken(user.Username);
            return Ok(new { Token = token });
        }

        // Endpoint to update an existing user
        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateUser([FromBody] EclipseLink.UserManagement.User user)
        {
            var existingUser = _userStore.GetUserById(user.User_Id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            _userStore.Save(user);
            return Ok("User updated successfully.");
        }

        // Endpoint to delete a user by username
        [Authorize]
        [HttpDelete("delete/{User_Id}")]
        public IActionResult DeleteUser([FromBody] EclipseLink.UserManagement.User user)
        {
            if(user == null)
            {
                return BadRequest("User is null.");
            }
            _userStore.Delete(user.Username);
            return Ok("User deleted successfully.");
        }

        // Private method to generate a JWT token for a user
        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
