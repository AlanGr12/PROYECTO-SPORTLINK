using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoSportlink.Models;

namespace proyectoSportlink.Controllers;

public class SessionController : Controller
{
    private readonly ILogger<SessionController> _logger;
    private readonly IWebHostEnvironment _env;

  public SessionController(ILogger<SessionController> logger, IWebHostEnvironment env)
{
    _logger = logger;
    _env = env;
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
       ViewBag.Clubes = BD.GetClubes(); 
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
                  return View("ElegirUser");

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
        IFormFile FotoPerfil, int idDeporte, DateTime fechaNacimiento, string usuario, string contraseña,
        string ubicacion, string genero)
    {
        DateTime hoy = DateTime.Now;
    edad = hoy.Year - fechaNacimiento.Year;

    // Si todavía no cumplió años este año, restamos uno
    if (hoy.Month < fechaNacimiento.Month || 
        (hoy.Month == fechaNacimiento.Month && hoy.Day < fechaNacimiento.Day))
    {
        edad--;
    }
 string nombreArchivo = Path.GetFileName(FotoPerfil.FileName);
        string rutaCarpeta = Path.Combine(_env.WebRootPath, "Imagenes");

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            FotoPerfil.CopyTo(stream);
        }

        string rutaRelativa = Path.Combine("Imagenes", nombreArchivo).Replace("\\", "/");

        BD.RegistrarJugador(nombre, apellido, telefono, edad, rutaRelativa, idDeporte, fechaNacimiento, usuario, contraseña, ubicacion, genero);
        return RedirectToAction("irLogInJugador","session");
    }

    [HttpPost]
    public IActionResult GuardarRegistroScout(string nombre, string apellido, int idClub, int telefono,
        IFormFile FotoPerfil, string usuario, string contraseña, string email)
    {
        string nombreArchivo = Path.GetFileName(FotoPerfil.FileName);
        string rutaCarpeta = Path.Combine(_env.WebRootPath, "Imagenes");

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            FotoPerfil.CopyTo(stream);
        }

        string rutaRelativa = Path.Combine("Imagenes", nombreArchivo).Replace("\\", "/");
        BD.RegistrarScout(nombre, apellido, idClub, telefono, rutaRelativa, usuario, contraseña, email);
        return RedirectToAction("irLogInScout","session");
    }
}
