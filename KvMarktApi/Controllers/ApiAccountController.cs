using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using KvMarktApi.Data;
using KvMarktApi.Models;
using KvMarktApi.Models.AccountViewModels;
using KvMarktApi.Responses;
using KvMarktApi.Services;



namespace KvMarktApi.Controllers
{
    [Authorize]
    [Route("api/account/[action]")]
    public class ApiAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly ApplicationDbContext _context;
        // private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ApiAccountController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        // [ApiValidationFilter]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(400, "Password or email have not the correct format."));
            }


            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.SingleOrDefaultAsync(r => r.Email == model.Email);
                var contributor = await _context.Contributor.Include(c => c.Association).SingleOrDefaultAsync(c => c.Email == model.Email);
                return Ok(new ApiOkResponse(new { 
                    token = GenerateJwtToken(model.Email, appUser),
                    email = model.Email,
                    firstname = contributor.Firstname,
                    lastname = contributor.Lastname,
                    id = contributor.Id,
                    association = contributor.Association
                }));
            }
            // if (result.RequiresTwoFactor)
            // {
            //     return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
            // }
            if (result.IsLockedOut)
            {
                // _logger.LogWarning("User account locked out.");
                return BadRequest(new ApiResponse(400, "User account locked out."));
            }
            return Unauthorized(); // new ApiResponse(401, "Invalid username or password.")

        }

        [HttpPost]
        [AllowAnonymous]
        [ApiValidationFilter]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            IdentityResult result = null;

            result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(new ApiOkResponse( GenerateJwtToken(model.Email, user) ));
            }
            var errors = result.Errors.Select(x => x.Description).ToArray();
            // return BadRequest(new ApiResponse(400, errors.ToString()));
            return BadRequest(new ApiBadRequestResponse(errors));
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginDto {

            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
