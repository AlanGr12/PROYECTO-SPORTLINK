public class Jugador{
public int idJugador{get;private set;}
public string Nombre{get;private set;}
public string Apellido{get;private set;}
public string Telefono{get;private set;}
public DateTime fechaNacimiento{get;private set;}
public string fotoPerfil{get;private set;}
public string Usuario{get;private set;}
public string Contraseña{get;private set;}
public string Ubicacion{get;private set;}
public string Genero{get;private set;}

public Jugador(){
    
}
public Jugador(int idjugador,string nombre,string apellido,string telefono,DateTime fechanacimiento,string fotoperfil,string usuario,string contraseña,string ubicacion,string genero)
{
this.idJugador= idjugador;
this.Nombre = nombre;
this.Apellido = apellido;
this.Telefono = telefono;
this.fechaNacimiento = fechanacimiento;
this.fotoPerfil = fotoperfil;
this.Usuario = usuario;
this.Contraseña = contraseña;
this.Ubicacion = ubicacion;
this.Genero = genero;



}



    
}