using Edukate.Models;
using Edukate.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edukate.Controllers
{
    public class AuthController : Controller
    {
        // TODO
        readonly IServiceCollection _services;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _singInManager;

        public AuthController(IServiceCollection services,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> singInManager)
        {
            _services = services;
            _userManager = userManager;
            _roleManager = roleManager;
            _singInManager = singInManager;
        }

        // Register

        // Get
        [HttpGet]
        public IActionResult Register()
            => View();

        // Post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Fullname = vm.Fullname,
                    Email = vm.Email,
                    UserName = vm.Username
                };

                var userCreationResult = await _userManager.CreateAsync(user, vm.Password);

                if (userCreationResult.Succeeded)
                {
                    await _singInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in userCreationResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid");
            }

            return View(vm);
        }

        // Login

        // Get
        [HttpGet]
        public IActionResult Login()
            => View();

        // Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Email == vm.UsernameOrEmail || x.UserName == vm.UsernameOrEmail);

                if (user != null)
                {
                    var userSingInResult = await _singInManager.PasswordSignInAsync(user, vm.Password, false, true);

                    if (userSingInResult.Succeeded) return RedirectToAction("Index", "Home");

                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User doesn't exist.");
                    return View(vm);
                }

            }

            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        /*public async Task CreateRoles()
        {
            // We just need to specify a unique role name to create a new role

            var gadminRole = await _roleManager.RoleExistsAsync("Gadmin123");

            if (!gadminRole)
            {
                await _roleManager.CreateAsync();
            }

            // Saves the role in the underlying AspNetRoles table
            IdentityResult result = await _roleManager.CreateAsync(gadminRole);

            if (result.Succeeded)
            {
                return RedirectToAction("index", "home");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }*/
    }
}
