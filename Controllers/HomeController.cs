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
        string tipo = HttpContext.Session.GetString("tipoUsuario");

        if (tipo == "jugador")
        {
           int id = int.Parse(HttpContext.Session.GetString("idJugador"));
            ViewBag.Usuario = BD.GetJugadorPorId(id);
        }
        else if (tipo == "scout")
        {
            int id = int.Parse(HttpContext.Session.GetString("idScout"));
            ViewBag.Usuario = BD.GetScoutPorId(id);
        }
        else
        {
            ViewBag.Usuario = null;
        }

        return View();
    }


    public IActionResult irPruebas(){
        return View("Pruebas");
    }

    public IActionResult irVideos(){
        return View("Videos");
    }

    public IActionResult irMensajes(){
        return View("Mensajes");
    }
    //public IActionResult Pruebas()
//{
    
        // Llamamos al método que obtiene los datos desde la BD
  //      List<Prueba> listaPruebas = BD.GetPruebas();

        // Guardamos esa lista dentro del ViewBag
    //    ViewBag.Pruebas = listaPruebas;

        // Enviamos la vista
      //  return View();
    
}

