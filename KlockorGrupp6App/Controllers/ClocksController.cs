using KlockorGrupp6App.Web.Views.Klockor;
using KlockorGrupp6App.Application.Clocks.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        User.Identity.Name.
        return View();
    }
}
