using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PhotoAlbum.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        //Constructor
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //Post: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            //Validate username and password
            if(username == _configuration["photos_username"] && password == _configuration["photos_password"])
            {
                // Create a list of claims identifying the user
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username), // unique ID
                    new Claim(ClaimTypes.Name, "Administrator"), // human readable name
                    //new Claim(ClaimTypes.Role, "Smuggler"), // could use roles if needed         
                };

                // Create the identity from the claims
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign-in the user with the cookie authentication scheme
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                //string? returnUrl = ("ReturnUrl"); //get the returnUrl from URL query string
                ViewData["returnUrl"] = returnUrl;

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
            }

                return RedirectToAction("Index", "Home");
        }

        //Get: /Account/Logout
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account"); //re-direct to /AccountController/Login
        }
    }
}
