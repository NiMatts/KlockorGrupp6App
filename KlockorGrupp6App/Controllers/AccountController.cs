using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Web.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        var model = await clockService.GetAllByUserIdAsync(user.Id);
        var viewModel = new MembersVM()
        {
            ClocksItems = [.. model.Select(c => new MembersVM.ClocksDataVM()
            {
                Brand = c.Brand,
                Model = c.Model,
                Id = c.Id,
            })]
        };

        return View(viewModel);
        //return View();
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Administrator")]
    public async Task <IActionResult> Admin()
    {
        var model = await clockService.GetAllAsync();
        var clocksItems = new List<AdminVM>();

        // Group clocks by user
        foreach (var group in model.GroupBy(c => c.CreatedByUserID))
        {
            var user = await userManager.FindByIdAsync(group.Key);

            var userClocks = group.Select(c => new AdminVM.ClocksDataVM
            {
                Owner = user?.Email ?? "Unknown",
                Brand = c.Brand,
                Model = c.Model,
                Id = c.Id
            }).ToList();

            clocksItems.Add(new AdminVM
            {
                UserId = group.Key,
                UserEmail = user?.Email ?? "Unknown",
                ClocksItems = userClocks
            });
        }

        return View(clocksItems);
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
