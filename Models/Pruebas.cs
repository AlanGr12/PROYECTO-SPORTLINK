public class Pruebas{
    public int idPrueba { get; private set; }
    public string Descripcion { get; private set; }
    public string Imagen { get; private set; }
    public string Categoria { get; private set; }
    public string Zona { get; private set; }
    public DateTime fechaPrueba { get; private set; }
    public string Genero { get; private set; }
    public string Deporte { get; private set; }   // ← desde tabla Deportes
    public string Nombre { get; private set; }    // ← desde tabla Clubes

    public bool inscripto;
public Pruebas()
{

}
  public Pruebas(
        int idPrueba,
        string descripcion,
        string imagen,
        string categoria,
        string zona,
        DateTime fechaPrueba,
        string genero,
        string deporte,
        string nombre
        )
    {
        this.idPrueba = idPrueba;
        this.Descripcion = descripcion;
        this.Imagen = imagen;
        this.Categoria = categoria;
        this.Zona = zona;
        this.fechaPrueba = fechaPrueba;
             this.Genero = genero;
        this.Deporte = deporte;
        this.Nombre = nombre;
        this.inscripto = false;

   
    }

}