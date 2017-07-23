using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sharetomato.Controllers
{
    public class AccountController : Controller
    {
        private IHostingEnvironment env;
        private DatabaseContext db;
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public AccountController(DatabaseContext db, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IHostingEnvironment env)
        {
            this.env = env;
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        /// <summary>
        /// GET: /Account/TwitterLogin, Challenge Twitter Login
        /// </summary>
        [HttpGet]
        public IActionResult TwitterLogin()
        {
            var provider = "Twitter";
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        /// <summary>
        /// Login or Create User
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            // Callback from Twitter?
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return BadRequest();
            }
            // Login Challenge
            var login = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true);
            if (login.Succeeded)
            {
                // Logined
                if (env.IsDevelopment())
                {
                    return Redirect("http://localhost:4200");
                }
                else
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                // Create User
                var user = new IdentityUser() { UserName = info.Principal.Identity.Name };
                await userManager.CreateAsync(user);
                var createUser = await userManager.AddLoginAsync(user, info);
                if (createUser.Succeeded)
                {
                    //// create user setting
                    //using (var db = new databasecontext())
                    //{
                    //    db.usersettings.add(new usersetting() { userid = user.id });
                    //    await db.savechangesasync();
                    //}

                    // Login and Redirect
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                // Error?
                return View();
            }
        }

        /// <summary>
        /// GET: /Account/LogOff, LogOff User
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
