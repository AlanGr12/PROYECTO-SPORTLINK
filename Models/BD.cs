using Microsoft.Data.SqlClient;
using Dapper;

static public class BD{

private static string _connectionString =  @"Server=localhost;Database=Sportlink;Integrated Security=True;TrustServerCertificate=True;";
public static int LoginJugador(string usuario, string contraseña)
{
int num = -1;

    string query = "SELECT idJugador FROM JUGADORES WHERE USUARIO = @pUsuario and Contraseña = @pContraseña";
    
    
using(SqlConnection connection = new SqlConnection(_connectionString))
{
    num = connection.QueryFirstOrDefault<int>(query, new { pUsuario = usuario,  pContraseña = contraseña });
    
    

}
return(num);
}

public static int LoginScout (string usuario,string contraseña)
{
int num = -1;
    string query = "SELECT idScout FROM SCOUTS WHERE USUARIO = @pUsuario and Contraseña = @pContraseña";  
using(SqlConnection connection = new SqlConnection(_connectionString))
{
    num = connection.QueryFirstOrDefault<int>(query, new { pUsuario = usuario,  pContraseña = contraseña});
}
return(num);
}
 public static void RegistrarJugador(string nombre, string apellido, int telefono, int edad, string fotoPerfil,
        int idDeporte, DateTime fechaNacimiento, string usuario, string contraseña, string ubicacion, string genero)
    {
        string query = @"INSERT INTO Jugadores 
                        (Nombre, Apellido, Telefono, Edad, FotoPerfil, idDeporte, FechaNacimiento, Usuario, Contraseña, Ubicacion, Genero)
                         VALUES (@pNombre, @pApellido, @pTelefono, @pEdad, @pFotoPerfil, @pIdDeporte, @pFechaNacimiento, @pUsuario, @pContraseña, @pUbicacion, @pGenero)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(query, new
            {
                pNombre = nombre,
                pApellido = apellido,
                pTelefono = telefono,
                pEdad = edad,
                pFotoPerfil = fotoPerfil,
                pIdDeporte = idDeporte,
                pFechaNacimiento = fechaNacimiento,
                pUsuario = usuario,
                pContraseña = contraseña,
                pUbicacion = ubicacion,
                pGenero = genero
            });
        }
    }

    public static List<Club> GetClubes()
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT * FROM Clubes";
        return db.Query<Club>(sql).ToList();
    }

}
    public static List<Pruebas> GetPruebas()
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT Pruebas.idPrueba, Pruebas.Descripcion,Pruebas.Imagen,Pruebas.Categoria,Pruebas.Zona,Pruebas.fechaPrueba,Pruebas.Genero,Deportes.deporte,Clubes.Nombre FROM Pruebas INNER JOIN Deportes ON Pruebas.idDeporte = Deportes.idDeporte INNER JOIN Clubes ON Pruebas.idClub = Clubes.idClub;";
        return db.Query<Pruebas>(sql).ToList();
    }

}

    public static List<int> GetInscrpcion(int idJugador)
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT idPrueba FROM JugadoresXPruebas WHERE idjugador = @pidJugador;";
        return db.Query<int>(sql, new{ pidJugador = idJugador}).ToList();
    }

}
    public static void RegistrarScout(string nombre, string apellido, int idClub, int telefono, string fotoPerfil,
        string usuario, string contraseña, string email)
    {
        string query = @"INSERT INTO Scouts 
                        (Nombre, Apellido, idClub, Telefono, FotoPerfil, Usuario, Contraseña, Email)
                        VALUES (@pNombre, @pApellido, @pIdClub, @pTelefono, @pFotoPerfil, @pUsuario, @pContraseña, @pEmail)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(query, new
            {
                pNombre = nombre,
                pApellido = apellido,
                pIdClub = idClub,
                pTelefono = telefono,
                pFotoPerfil = fotoPerfil,
                pUsuario = usuario,
                pContraseña = contraseña,
                pEmail = email
            });
        }
    }

    public static Jugador GetJugadorPorId(int id){
        string query = @"Select * FROM Jugadores WHERE idJugador = @jid";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {   
             return db.QueryFirstOrDefault<Jugador>(query, new { jid = id,});
        }

        

    }

    public static Scouter GetScoutPorId(int id){
        string query = @"Select * FROM Scouts WHERE idScout = @sid";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {   
             return db.QueryFirstOrDefault<Scouter>(query, new { sid = id,});
        }

    }


 public static void RegistrarPrueba(string Descripcion,string Imagen,string Categoria,string Zona,DateTime fechaPrueba,string Genero,int idDeporte,int idClub)
    {
        string query = @"INSERT INTO Pruebas 
                        (Descripcion,Imagen,Categoria,Zona,fechaPrueba,idClub,idDeporte,Genero)
                        VALUES (@pDescripcion, @pImagen, @pCategoria, @pZona, @pfechaPrueba, @pidClub, @pidDeporte, @pGenero)";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(query, new
            {
                pDescripcion = Descripcion,
                pImagen = Imagen,
                pCategoria = Categoria,
                pZona = Zona,
                pfechaPrueba = fechaPrueba,
                pidClub = idClub,
                pidDeporte = idDeporte,
                pGenero = Genero
            });
        }
    }   

     public static void inscripcionPrueba(int idPrueba,int idJugador)
    {

string query = @"INSERT INTO JugadoresXPruebas (idJugador,idPrueba) VALUES (@pidJugador, @pidPrueba)";

using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(query, new
            {
             pidJugador = idJugador,  
             pidPrueba = idPrueba
            });
        }


    } 
    public static void RegistrarVideo(string Titulo,string Video,int idJugador,int idDeporte,string Comentario,int meGusta)
    {

string query = @"INSERT INTO Videos (Titulo,Video,idJugador,idDeporte,Comentario,meGusta) VALUES (@pTitulo,@pVideo,@pidJugador,@pidDeporte,@pComentario,@pMegusta)";
using (SqlConnection db = new SqlConnection(_connectionString))
        {
            db.Execute(query, new
            {
             pTitulo = Titulo,  
             pVideo = Video,
             pidJugador = idJugador,
             pidDeporte = idDeporte,
             pComentario = Comentario,
             pmeGusta = meGusta
            
            });

        }



    }
    public static List<Videos> getVideosXId(int idJugador)
    {

string query = @"Select * FROM Videos WHERE idJugador = @pidJugador";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {   
             return db.Query<Videos>(query, new{ pidJugador = idJugador}).ToList();
        }



    }
        public static List<Videos> GetVideos()
{
    using (SqlConnection db = new SqlConnection(_connectionString))
    {
        string sql = "SELECT Videos.*, Jugadores.Nombre as NombreJugador FROM Videos INNER JOIN Jugadores on Videos.idJugador=Jugadores.idJugador";
        return db.Query<Videos>(sql).ToList();
    }

}

}

