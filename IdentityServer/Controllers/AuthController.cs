using IdentityServer.Interfaces;
using IdentityServer.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(IAuthService authService,
            IIdentityServerInteractionService interactionService,
            SignInManager<AppUser> signInManager) =>
            (_authService, _interactionService, _signInManager) = (authService, interactionService, signInManager);

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var viewModel = new LoginViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _authService.GetUserAsync(viewModel.Username, viewModel.Password);
                    if (user != null)
                    {
                        await _signInManager.SignInAsync(user, false);

                        Redirect(viewModel.ReturnUrl);
                    }
                    ModelState.AddModelError("", "Некорректное имя пользователя или пароль");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
