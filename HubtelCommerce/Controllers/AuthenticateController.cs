using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HubtelCommerce.Models;
using HubtelCommerce.ResponseModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HubtelCommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class AuthenticateController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IConfiguration configuration, UserManager<User> userManager
            , ILogger<AuthenticateController> logger)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, credentials.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, credentials.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = GenerateToken(claims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUp model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                _logger.LogError($"Error creating user! {result}");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user logs and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value));

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT:ValidIssuer").Value,
                audience: _configuration.GetSection("JWT:ValidAudience").Value,
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

