using KlockorGrupp6App.Web.Views.Klockor;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KlockorGrupp6App.Domain;
using KlockorGrupp6App.Application.Clocks.Services;

namespace KlockorGrupp6App.Web.Controllers;

public class ClocksController(IClockService service) : Controller
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

    [HttpPost("create")]
    public IActionResult Create(CreateVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var clock = new Clock
        {
            Brand = viewModel.Brand,
            Model = viewModel.Model,
            Price = viewModel.Price,
            Year = viewModel.Year,
        };

        service.Add(clock);

        return RedirectToAction(nameof(Index));
    }
}
