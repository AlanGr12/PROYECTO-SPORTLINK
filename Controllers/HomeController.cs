using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoSportlink.Models;

namespace proyectoSportlink.Controllers;

public class HomeController : Controller
{
 
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

  public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
{
    _logger = logger;
    _env = env;
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
     public IActionResult irSubirPrueba(){
        return View("subirPrueba");
    }

    public IActionResult irMensajes(){
        return View("Mensajes");
    }
   public IActionResult GuardarRegistroPruebas(string Descripcion,IFormFile Imagen,string Categoria,string Zona,DateTime fechaPrueba,string Genero,int idDeporte,int idClub)
   {
string nombreArchivo = Path.GetFileName(Imagen.FileName);
        string rutaCarpeta = Path.Combine(_env.WebRootPath, "Imagenes");

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            Imagen.CopyTo(stream);
        }

        string rutaRelativa = Path.Combine("Imagenes", nombreArchivo).Replace("\\", "/");
        BD.RegistrarPrueba(Descripcion,rutaRelativa, Categoria, Zona, fechaPrueba, Genero, idDeporte, idClub);

 return RedirectToAction("irPruebas","Home");
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

