using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Clocks.Services;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Infrastructure.Persistance;
using KlockorGrupp6App.Web.Views.Klockor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlockorGrupp6App.Web.Controllers;

public class ClocksController(IClockService service, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    { 
        var model = service.GetAll();
        var viewModel = new IndexVM()
        {
            ClocksItems = model.Select(c => new IndexVM.ClocksDataVM()
            {
                Brand = c.Brand,
                Model = c.Model,
                Id = c.Id,
            }).ToArray()
        };

        return View(viewModel);
    }

    [HttpGet("details/{id}")]
    public IActionResult Details(int id)
    {
        return View();
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    //[HttpGet("delete/{id}")]
    //[Authorize]
    //public IActionResult Delete(int id)
    //{
    //    var clock = service.GetById(id);
    //    service.Remove(clock);
    //    return RedirectToAction(nameof(Index));
    //}
    [HttpPost("delete")]
    [Authorize]
    public IActionResult Delete(int id)
    {
        var clock = service.GetById(id);
        service.Remove(clock);
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
            Year = viewModel.Year,
            CreatedByUserID = user.Id
        };

        service.Add(clock);

        return RedirectToAction(nameof(Index));
    }
}
