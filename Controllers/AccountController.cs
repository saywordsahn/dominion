using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using DominionWeb;
using DominionWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace SignalRAuthenticationSample.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private static readonly SigningCredentials SigningCreds = new SigningCredentials(Startup.SecurityKey, SecurityAlgorithms.HmacSha256);

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToPage("/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] LoginInfo loginInfo)
        {
            try
            {
                // Check the password but don't "sign in" (which would set a cookie)
                var user = await _signInManager.UserManager.FindByEmailAsync(loginInfo.Email);
                if (user == null)
                {
                    return Json(new
                    {
                        error = "Login failed"
                    });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginInfo.Password, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var principal = await _signInManager.CreateUserPrincipalAsync(user);
                    var token = new JwtSecurityToken(
                        "SignalRAuthenticationSample",
                        "SignalRAuthenticationSample",
                        principal.Claims,
                        expires: DateTime.UtcNow.AddDays(30),
                        signingCredentials: SigningCreds);
                    return Json(new
                    {
                        token = _tokenHandler.WriteToken(token)
                    });
                }
                else
                {
                    return Json(new
                    {
                        error = result.IsLockedOut ? "User is locked out" : "Login failed"
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = ex.ToString()
                });
            }
        }

        [HttpPost]
        public async Task<string> Register([FromBody] LoginInfo loginInfo)
        {
            var user = new ApplicationUser {UserName = loginInfo.Email, Email = loginInfo.Email };
            var result = await _userManager.CreateAsync(user, loginInfo.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                // We don't log the user in because we don't want to set a cookie in this sample.
                return "Successfully registered!";
            }

            //change this to try/catch
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return "ERROR: Could not register.";
        }

        // If we got this far, something failed, redisplay form
//            return Page();

        public class LoginInfo
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}