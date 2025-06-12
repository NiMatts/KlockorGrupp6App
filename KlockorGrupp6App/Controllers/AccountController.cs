using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Identity;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Web.Views.Klockor;
namespace KlockorGrupp6App.Web.Controllers;

public class AccountController(IUserService userService, UserManager<ApplicationUser> userManager, IClockService clockService) : Controller
{
    //[HttpGet("")]
    [HttpGet("members")]
    [Authorize]
    public async Task <IActionResult> Members()
    {
        var user = await userManager.FindByEmailAsync(User.Identity.Name);
        var model = await clockService.GetAllByUserId(user.Id);
        var viewModel = new MembersVM()
        {
            ClocksItems = model.Select(c => new MembersVM.ClocksDataVM()
            {
                Brand = c.Brand,
                Model = c.Model,
                Id = c.Id,
            }).ToArray()
        };

        return View(viewModel);
        //return View();
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Administrator")]
    public async Task <IActionResult> Admin()
    {
        await Task.Delay(2000);
        var user = await userManager.GetUserAsync(User);
        var model = new AdminVM
        {
            Username = user.Email
        };
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
        var user = await userManager.FindByEmailAsync(viewModel.Username);
        var roles = await userManager.GetRolesAsync(user);
        
        if (roles.Contains("Administrator"))
            return RedirectToAction(nameof(Admin));
        else
            return RedirectToAction(nameof(Members));
    }

    [HttpGet("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await userService.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

}
