using Forum.API.Configuration;
using Forum.API.ViewModels;
using Forum.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppSettings appSettings;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignUpUser signupUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    code = Response.StatusCode,
                    errors = ModelState.Values.SelectMany(e => e.Errors)
                });

            var user = new ApplicationUser
            {
                UserName = signupUser.Email,
                Email = signupUser.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, signupUser.Password);

            if (!result.Succeeded)
                return BadRequest(new
                {
                    code = Response.StatusCode,
                    errors = result.Errors
                });

            await signInManager.SignInAsync(user, false);

            return Ok(new
            {
                code = Response.StatusCode,
                authenticated = true,
                token = await GenerateToken(signupUser.Email)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    code = Response.StatusCode,
                    authenticated = false,
                    errors = ModelState.Values.SelectMany(e => e.Errors)
                });

            var result = await signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
                return Ok(new
                {
                    code = Response.StatusCode,
                    authenticated = true,
                    token = await GenerateToken(loginUser.Email)
                });

            return BadRequest(new
            {
                code = Response.StatusCode,
                authenticated = false,
                message = "Usuário ou senha inválidos"
            });
        }

        private async Task<string> GenerateToken(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = appSettings.Issuer,
                Audience = appSettings.ValidAt,
                Expires = DateTime.UtcNow.AddHours(appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
