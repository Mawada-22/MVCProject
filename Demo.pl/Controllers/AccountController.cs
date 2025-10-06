using Demo.DAL.Entites.Identitiy;
using Demo.pl.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Packaging.Signing;

namespace Demo.pl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userMager;
        private readonly SignInManager<ApplicationUser> _signInManger;

        public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser>signInManager)
        {
            _userMager = userManager;
            _signInManger=signInManager;
        }
        
        //signup
        #region signup 
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            _userMager.FindByNameAsync(registerViewModel.UserName).Wait();

            var User = new ApplicationUser()
            {
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                SecondName = registerViewModel.SecondName,
                IsAgree = registerViewModel.IsAgree,
                UserName = registerViewModel.UserName,

            };

            if(User is { })
            {
                ModelState.AddModelError(nameof(registerViewModel.UserName), "This Name Is Used");

            }

            var Res = await _userMager.CreateAsync(User, registerViewModel.Password);
            if (Res.Succeeded)
                return RedirectToAction("LogIn");
            foreach(var  error in Res.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(registerViewModel);
;         }

        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(loginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            var User = await _userMager.FindByEmailAsync(loginViewModel.Email);

            if(User is { })
            {
                var flag = await _userMager.CheckPasswordAsync(User, loginViewModel.Password);
                if (flag)
                {
                    var res = await _signInManger.PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false);
                   if(res.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account is Locked");
                   if(res.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account is not Confirmed");

                    if (res.Succeeded)
                    {
                        return RedirectToAction((nameof(HomeController.Index)),"Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attemp");
            return View(loginViewModel);
        }
        #endregion
    }
}
