using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;

using System.Text.Json;


namespace i_App.Controllers
{
    public class LoginController : Controller
    {
        private AppState _appState;
        private readonly AuthorizeRepository _repo;


       

        public LoginController(AuthorizeRepository repo, AppState appState)
        {
            _repo = repo;
            _appState = appState;   
        }

        [HttpPost("/account/login")]
        public async Task<IActionResult> Login(UserCredentials credentials)
        {
            var result = await _repo.GetUser(credentials.Username);
            if (result) {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "HidalgoCounty"))
                {
                    bool isValid = pc.ValidateCredentials(credentials.Username, credentials.Password);
                    System.Diagnostics.Debug.WriteLine($"IS VALID {isValid}");
                    if (isValid)
                    {
                        //Save the Account information to the app state
                        var Account = await _repo.GetAccount(credentials.Username);

                        //save if remember or not
                        var IsRember = !string.IsNullOrEmpty(credentials.Remember);

                        //if remember was press need to save in local storage..
                        if (IsRember) {
                            DateTime today = DateTime.Now;
                            DateTime answer = today.AddDays(7);
                            Account.Expire = answer;
                        }

                        _appState.ViewAccount = Account;

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, credentials.Username),
                            new Claim(ClaimTypes.Role, "")
                        };

                        // Create the Claim Principal
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        if (IsRember)
                        {
                            var authProperties = new AuthenticationProperties
                            {
                                //AllowRefresh = <bool>,
                                // Refreshing the authentication session should be allowed.

                                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                                // The time at which the authentication ticket expires. A 
                                // value set here overrides the ExpireTimeSpan option of 
                                // CookieAuthenticationOptions set with AddCookie.

                                IsPersistent = true
                                // Whether the authentication session is persisted across 
                                // multiple requests. When used with cookies, controls
                                // whether the cookie's lifetime is absolute (matching the
                                // lifetime of the authentication ticket) or session-based.

                                //IssuedUtc = <DateTimeOffset>,
                                // The time at which the authentication ticket was issued.

                                //RedirectUri = <string>
                                // The full path or absolute URI to be used as an http 
                                // redirect response value.
                            };
                            //Generate Cookie by suing SignInAsync is a funciton for context.
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
                        }
                        else {
                            //Generate Cookie by suing SignInAsync is a funciton for context.
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                        }

                      

                        if (IsRember) { 
                            return LocalRedirect("/login/Handler/Account");
                        }
                        return LocalRedirect("/Dashboard");
                    }
                    else {
                        return LocalRedirect("/login/Username and Password Incorrect");
                    }
                }
                   
            }
            return LocalRedirect("/login/Error Code 404");


        }



        [HttpGet("/account/setup")]
        public async Task<IActionResult> Setup()
        {
            var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, _appState.ViewAccount.UserName ?? ""),
                            new Claim(ClaimTypes.Role, "")
                        };

            // Create the Claim Principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //Generate Cookie by suing SignInAsync is a funciton for context.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return LocalRedirect("/Dashboard");

        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }

    public class UserCredentials
    {
        [Required]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string Remember { get; set; }
    }

}
