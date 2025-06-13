using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Views.Clocks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlockorGrupp6App.Web.Controllers;

public class ClocksController(IClockService service, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    { 
        var model = await service.GetAllAsync();
        var viewModel = new IndexVM()
        {
            ClocksItems = [.. model.Select(c => new IndexVM.ClocksDataVM()
            {
                Brand = c.Brand,
                Model = c.Model,
                Id = c.Id,
            })]
        };

        return View(viewModel);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var model = await service.GetByIdAsync(id);

        DetailsVM viewModel = new()
        {
            Brand = model.Brand,
            Model = model.Model,
            Price = model.Price,
            Year = model.Year,
        };

        return View(viewModel);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("delete")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var clock = await service.GetByIdAsync(id);
        await service.RemoveAsync(clock);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var user = await userManager.FindByEmailAsync(User.Identity.Name);

        var clock = new Clock
        {
            Brand = viewModel.Brand,
            Model = viewModel.Model,
            Price = viewModel.Price,
            Year = new DateTime((int)viewModel.Year, 1, 1),            
            CreatedByUserID = user.Id
        };

        await service.AddAsync(clock);

        return RedirectToAction(nameof(Index));
    }
}
