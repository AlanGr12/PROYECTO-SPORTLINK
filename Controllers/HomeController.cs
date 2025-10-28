using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoSportlink.Models;

namespace proyectoSportlink.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Index");
    }

    public IActionResult Pruebas()
{
    
        // Llamamos al método que obtiene los datos desde la BD
        List<Prueba> listaPruebas = BD.GetPruebas();

        // Guardamos esa lista dentro del ViewBag
        ViewBag.Pruebas = listaPruebas;

        // Enviamos la vista
        return View();
    
}
}
