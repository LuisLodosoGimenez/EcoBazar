namespace backend.Models
{
   public class Producto{

    public int? Id{get;set;}
    public int Precio_cents{get;set;}
    public int Unidades{get;set;}
    public Vendedor vendedor{get;set;}
    public Articulo articulo{get;set;}

    public Producto(int precio_cents, int unidades, Vendedor vendedor, Articulo articulo){
        
        this.Precio_cents = precio_cents;
        this.Unidades = unidades;
        this.vendedor = vendedor;
        this.articulo = articulo;
    }
   } 
}
