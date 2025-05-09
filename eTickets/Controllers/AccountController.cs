using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;

        public AccountController(SignInManager<Users> signInManager, UserManager<Users>userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
         public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Movie");
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Users users = new Users
                {
                    FullName = viewModel.Name,
                    Email = viewModel.Email,
                    UserName = viewModel.Email
                };
                var result = await userManager.CreateAsync(users, viewModel.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(users, "User");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(viewModel);
                }
            }
           
            return View(viewModel);
        }


        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(VerifyEmailViewModel verifyViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(verifyViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Email not found");
                    return View(verifyViewModel);
                }
                else
                {
                    return RedirectToAction("ChangePassword", "Account", new { username = user.UserName });
                   
                }
            }
            return View(verifyViewModel);
        }


          public IActionResult ChangePassword(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerifyEmail","Account");
            }
            return View(new ChangePasswordViewModel { Email= username});
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(changePasswordModel.Email);
                if (user != null)
                {
                    var result = await userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddPasswordAsync(user, changePasswordModel.NewPassword);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View(changePasswordModel);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Not Found");
                        return View(changePasswordModel);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Something's wrong. Try Again!");
                    return View(changePasswordModel);
                }

            }
            return View(changePasswordModel);
        }



        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movie");
        }

    }
}
