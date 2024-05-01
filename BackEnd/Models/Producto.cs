namespace backend.Models
{
   public class Producto{

    private int Id;
    private int Precio_cents;
    private int Unidades;
    private int Id_usuario;
    private int Id_articulo;
    private Vendedor vendedor;
    private Articulo articulo;

    public Producto(int id, int precio_cents, int unidades, int id_usuario, int id_articulo, Vendedor vendedor, Articulo articulo){
        this.Id = id;
        this.Precio_cents = precio_cents;
        this.Unidades = unidades;
        this.Id_usuario = id_usuario;
        this.Id_articulo = id_articulo;
        this.vendedor = vendedor;
        this.articulo = articulo;
    }

    public int getId(){
        return this.Id;
    }
    
    public int getId_Articulo(){
        return this.Id_articulo;
    }

    public int getPrecio(){
        return this.Precio_cents;
    }

   } 
}
