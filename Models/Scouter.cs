public class Scouter{
public int idScout {get;private set;}
public string Nombre{get;private set;}
public string Apellido{get;private set;}
public string Club{get;private set;}
public string Telefono{get;private set;}
public string fotoPerfil{get;private set;}
public string Usuario{get;private set;}
public string Password{get;private set;}
public string Email{get;private set;}

public Scouter(int idscout,string nombre,string apellido,string club,string telefono,string fotoperfil,string usuario,string password,string email)
{
this.idScout = idscout;
this.Nombre = nombre;
this.Apellido = apellido;
this.Club = club;
this.Telefono = telefono;
this.fotoPerfil = fotoperfil;
this.Usuario = usuario;
this.Password = password;
this.Email = email;



}


}
