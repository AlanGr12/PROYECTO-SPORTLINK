using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoSportlink.Models;

namespace proyectoSportlink.Controllers;

public class SessionController : Controller
{
    private readonly ILogger<SessionController> _logger;

    public SessionController(ILogger<SessionController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("id") !=null)
        {
            // traigo de session el tipo y busco con el id en la tabla segun el tipo
            
  
        string tipo = HttpContext.Session.GetString("tipoUsuario");

      
        int id = int.Parse(HttpContext.Session.GetString("id"));

    
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
        return View("Index");
    }


    public IActionResult irLogInJugador(){
        
        return View("InicioSesionJugador");

    }

    public IActionResult irLogInScout(){

        return View("InicioSesionScout");

    }
    public IActionResult irRegistrarJugador(){
        return View("RegistrarJugador");

    }


    public IActionResult ElegirUser()
{
    return View("ElegirUser");
}

    public IActionResult irRegistrarScout(){
     return View("RegistrarScout");

    }

    public IActionResult irUser(){
        return View("ElegirUser");
    }

     [HttpPost]
    public IActionResult guardarInicio(string Usuario, string Contraseña)
    {
        int idJugador = BD.LoginJugador(Usuario, Contraseña);
        if (idJugador>0)
        {
            HttpContext.Session.SetString("id", idJugador.ToString());
            HttpContext.Session.SetString("tipoUsuario", "jugador");
            return RedirectToAction("Index", "Home");   

        }
        else
        {
            int idScout = BD.LoginScout(Usuario, Contraseña);
            if (idScout >0)
            {
                HttpContext.Session.SetString("id", idScout.ToString());
                HttpContext.Session.SetString("tipoUsuario", "scout");
                return RedirectToAction("Index", "Home");

            }
            else{
                 ViewBag.Error = "Usuario o contraseña incorrectos.";
                return RedirectToAction("ElegirUser", "Session");

            }

        }


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
        IFormFile fotoPerfil, int idDeporte, DateTime fechaNacimiento, string usuario, string password,
        string ubicacion, string genero)
    {
        //string rutaRelativa = GuardarImagen(fotoPerfil);
        string rutaRelativa = "s";
        Console.WriteLine(usuario);
        Console.WriteLine(password);
        BD.RegistrarJugador(nombre, apellido, telefono, edad, rutaRelativa, idDeporte, fechaNacimiento, usuario, password, ubicacion, genero);
        return RedirectToAction("Login");
    }


    public IActionResult RegistroScout()
    {
        ViewBag.Clubes = BD.GetClubes();
        return View();
    }

    [HttpPost]
    public IActionResult GuardarRegistroScout(string nombre, string apellido, int idClub, int telefono,
        IFormFile fotoPerfil, string usuario, string contraseña, string email)
    {
        //string rutaRelativa = GuardarImagen(fotoPerfil);
        string rutaRelativa =  "k";
        BD.RegistrarScout(nombre, apellido, idClub, telefono, rutaRelativa, usuario, contraseña, email);
        return RedirectToAction("Login");
    }
}
