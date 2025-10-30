using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoSportlink.Models;

namespace proyectoSportlink.Controllers;

public class sessionController : Controller
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

     [HttpPost]
    public IActionResult GuardarLogin(string usuario, string contraseña)
    {
        int idJugador = BD.LoginJugador(usuario, contraseña);
        int idScout = BD.LoginScout(usuario, contraseña);

        if (idJugador > 0)
        {
            HttpContext.Session.SetString("idJugador", idJugador.ToString());
            HttpContext.Session.SetString("tipoUsuario", "jugador");
            return RedirectToAction("Index", "Home");
        }
        else if (idScout > 0)
        {
            HttpContext.Session.SetString("idScout", idScout.ToString());
            HttpContext.Session.SetString("tipoUsuario", "scout");
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View("Login");
    }


    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();  
        return RedirectToAction("Login");
    }

    public IActionResult RegistroJugador()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GuardarRegistroJugador(string nombre, string apellido, int telefono, int edad,
        IFormFile fotoPerfil, int idDeporte, DateTime fechaNacimiento, string usuario, string contraseña,
        string ubicacion, string genero)
    {
        string rutaRelativa = GuardarImagen(fotoPerfil);
        BD.RegistrarJugador(nombre, apellido, telefono, edad, rutaRelativa, idDeporte, fechaNacimiento, usuario, contraseña, ubicacion, genero);
        return RedirectToAction("Login");
    }

    // 🔹 REGISTRO SCOUT
    public IActionResult RegistroScout()
    {
        ViewBag.Clubes = BD.GetClubes();
        return View();
    }

    [HttpPost]
    public IActionResult GuardarRegistroScout(string nombre, string apellido, int idClub, int telefono,
        IFormFile fotoPerfil, string usuario, string contraseña, string email)
    {
        string rutaRelativa = GuardarImagen(fotoPerfil);
        BD.RegistrarScout(nombre, apellido, idClub, telefono, rutaRelativa, usuario, contraseña, email);
        return RedirectToAction("Login");
    }
}
