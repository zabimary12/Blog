using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Auth;
using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, IOptions<AuthOptions> authOptions)
        {
            _userService = userService;
            _authOptions = authOptions;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            var user = await AuthenticateUser(request.Email, request.Password);
            if (user != null)
            {
                var token = GenerateJwt(user);
                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        private async Task<UserDto> AuthenticateUser(string email, string password)
        {
            IEnumerable<UserDto> users = await _userService.GetAll();
            var allUsers = users.ToList();
            return allUsers.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        private string GenerateJwt(UserDto userDto)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, userDto.Email),
                new(JwtRegisteredClaimNames.Sub, userDto.Id.ToString()),
                new("Password", userDto.Password),
                new("Name", userDto.Name),
                new("Surname", userDto.Surname),
                new(ClaimsIdentity.DefaultRoleClaimType, userDto.Role.ToString())
            };


            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}