using Microsoft.Data.SqlClient;
using Dapper;

static public class BD{

private static string _connectionString =  @"Server=localhost;Database=Sportlink;Integrated Security=True;TrustServerCertificate=True;";
public static int LoginJugador(string contraseña, string usuario)
{
int num = -1;

    string query = "SELECT idJugador FROM JUGADORES WHERE USUARIO = @pUsuario and PASSWORD = @pContraseña";
    
    
using(SqlConnection connection = new SqlConnection(_connectionString))
{
    num = connection.QueryFirstOrDefault<int>(query, new { pUsuario = usuario,  pContraseña = contraseña });
    
    

}
return(num);
}

public static int LoginScout (string contraseña,string usuario)
{
int num = -1;
    string query = "SELECT idScout FROM SCOUTS WHERE USUARIO = @pUsuario and PASSWORD = @pContraseña";  
using(SqlConnection connection = new SqlConnection(_connectionString))
{
    num = connection.QueryFirstOrDefault<int>(query, new { pUsuario = usuario,  pContraseña = contraseña });
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

    
}

