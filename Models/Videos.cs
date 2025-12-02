public class Videos{
public int idVideo {get;private set;}
public string Titulo{get;private set;}
public string Video{get;private set;}
public int idJugador{get;private set;}
public int idDeporte{get;private set;}
public string Comentario{get;private set;}
public int meGusta{get;private set;}
public string NombreJugador{get;private set;}


public Videos ()
{



    
}
public Videos(int idvideo,string titulo,string video,int idjugador,int iddeporte,string comentario,int megusta)
{
this.idVideo= idvideo;
this.Titulo = titulo;
this.Video=video;
this.idJugador=idjugador;
this.idDeporte=iddeporte;
this.Comentario=comentario;
this.meGusta = megusta;



}


}
