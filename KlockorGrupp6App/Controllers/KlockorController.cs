using Microsoft.AspNetCore.Mvc;

namespace KlockorGrupp6App.Web.Controllers;

public class KlockorController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
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

    //[HttpPost("create")]
    //public IActionResult Create()
    //{
    //    return View();
    //}
}
