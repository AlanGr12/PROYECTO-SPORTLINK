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
         if (HttpContext.Session.GetString("id") !=null)
        {
            // traigo de session el tipo y busco con el id en la tabla segun el tipo
            
  
        string tipo = HttpContext.Session.GetString("tipoUsuario");

      
        int id = int.Parse(HttpContext.Session.GetString("id"));

        List<Pruebas> pruebas = BD.GetPruebas();
        if (tipo == "jugador")
        {
            Jugador j = BD.GetJugadorPorId(id);
            ViewBag.Usuario = j;
             List<int> inscripciones = BD.GetInscrpcion(id);
           foreach(Pruebas item in pruebas)
           {
            for(int i = 0; i < inscripciones.Count; i++)
            {
                if(item.idPrueba == inscripciones[i])
                {
                    item.inscripto = true;
                }
           }
                      ViewBag.Pruebas = pruebas;
           }
        }else if (tipo == "scout")
        {
            Scouter s = BD.GetScoutPorId(id);
            ViewBag.Usuario = s;
        }

            
          
            
           ViewBag.TipoUsuario = HttpContext.Session.GetString("tipoUsuario");
           
        }
          
    return View("Pruebas");
    }  
    


    public IActionResult irVideos(){
         if (HttpContext.Session.GetString("id") !=null)
        {
            // traigo de session el tipo y busco con el id en la tabla segun el tipo
            
  
        string tipo = HttpContext.Session.GetString("tipoUsuario");

      
        int id = int.Parse(HttpContext.Session.GetString("id"));

    
        if (tipo == "jugador")
        {
            Jugador j = BD.GetJugadorPorId(id);
            ViewBag.VideosJugador = BD.getVideosXId(id);
            ViewBag.Usuario = j;
        }
        else if (tipo == "scout")
        {
            Scouter s = BD.GetScoutPorId(id);
            ViewBag.Usuario = s;
        }

        }
        ViewBag.TipoUsuario = HttpContext.Session.GetString("tipoUsuario");
        return View("Videos");
    }
     public IActionResult irSubirPrueba(){
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
        return View("subirPrueba");
    }

    public IActionResult irMensajes(){
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
        return View("Mensajes");
    }

    public IActionResult irSubirVideo(){
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
        
        return View("subirVideo");
    }

  [HttpPost]
        public IActionResult inscribirsePrueba(int idPrueba,int idJugador){
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
        BD.inscripcionPrueba(idPrueba,idJugador);
        
        

        return View("anotarsePrueba");
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

   public IActionResult GuardarRegistroVideos(string Titulo,IFormFile Video,int idJugador,int idDeporte,string Comentario,int meGusta)
   {
string nombreArchivo = Path.GetFileName(Video.FileName);
        string rutaCarpeta = Path.Combine(_env.WebRootPath, "Videos");

        if (!Directory.Exists(rutaCarpeta))
            Directory.CreateDirectory(rutaCarpeta);

        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            Video.CopyTo(stream);
        }

        string rutaRelativa = Path.Combine("videos", nombreArchivo).Replace("\\", "/");
        BD.RegistrarVideo(Titulo, rutaRelativa,idJugador, idDeporte, Comentario, meGusta);

 return RedirectToAction("irVideos","Home");
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

