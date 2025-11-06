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
    string idStr = HttpContext.Session.GetString("id");
    string tipo = HttpContext.Session.GetString("tipoUsuario");

    if (!string.IsNullOrEmpty(idStr) && !string.IsNullOrEmpty(tipo))
    {
        int id = int.Parse(idStr);

        if (tipo == "jugador")
        {
            Jugador j = BD.GetJugadorPorId(id);
            ViewBag.Usuario = j;
        }
        else if (tipo == "scout")
        {
            Scouter s = BD.GetScoutPorId(id);
            ViewBag.Usuario = s;
        }
    }
    else
    {
        ViewBag.Usuario = null; // no hay nadie logueado
    }

    return View("Index");
}


    public IActionResult irPruebas(){
           ViewBag.Pruebas = BD.GetPruebas(); 
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

