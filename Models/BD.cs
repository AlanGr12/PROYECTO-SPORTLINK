using Microsoft.Data.SqlClient;
using Dapper;

static public class BD{

private static string _connectionString = @"Server=localhost;Database=TP06 repaso;Integrated Security=True;TrustServerCertificate=True;";
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
    string query = "SELECT ID FROM SCOUTS WHERE USUARIO = @pUsuario and PASSWORD = @pContraseña";  
using(SqlConnection connection = new SqlConnection(_connectionString))
{
    num = connection.QueryFirstOrDefault<int>(query, new { pUsuario = usuario,  pContraseña = contraseña });
}
return(num);
}
public static string RegistrarseJugadores(string Username,string Password,string Nombre,string Apellido,string deporte,string Telefono, DateTime fechaNacimiento,string fotoPerfil,string Ubicacion,string Genero)
{
string hh = "Messi";

string query = "INSERT INTO JUGADORES (Username,Password,Nombre,Apellido,Foto,FechaUltimoLogin) VALUES (@pUsername, @pPassword,@pNombre,@pApellido,@pFoto,@pFechaUltimoLogin)";
using (SqlConnection connection = new SqlConnection (_connectionString))
{
string query2 = "SELECT Username FROM Usuario WHERE Username = @pUsuario";
hh= connection.QueryFirstOrDefault<string>(query2, new{pUsuario = Username});
if(hh == null || hh == "Messi")
{
connection.Execute(query,new {pUsername= Username, pPassword = Password,pNombre = Nombre,pApellido = Apellido, pFoto = Foto , pFechaUltimoLogin = FechaUltimoLogin});
hh = "hola";
}   
}
return hh;
}



}