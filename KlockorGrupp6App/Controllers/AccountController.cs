using Microsoft.AspNetCore.Mvc;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using System.Data;
namespace KlockorGrupp6App.Web.Controllers;

public class AccountController(IUserService userService) : Controller
{
    //[HttpGet("")]
    [HttpGet("members")]
    [Authorize]
    public IActionResult Members()
    {
        return View();
    }
    //Implementera isAdmin i inloggad 
    [HttpGet("admin")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Admin()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Try to register user
        var userDto = new UserProfileDto(viewModel.Email, viewModel.FirstName, viewModel.LastName);
        var result = await userService.CreateUserAsync(userDto, viewModel.Password,viewModel.isAdmin);
        if (!result.Succeeded)
        {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Check if credentials is valid (and set auth cookie)
        var result = await userService.SignInAsync(viewModel.Username, viewModel.Password);
        if (!result.Succeeded)
        {
            // Show error
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }

        // Redirect user
        return RedirectToAction(nameof(Admin));
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await userService.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

}
