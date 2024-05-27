using System.Collections.ObjectModel;
using backend.Logica;

namespace backend.Models
{
   public class Producto{

    public int? Id{get;set;}
    public int Precio_cents{get;set;}
    public int Unidades{get;set;}
    public Vendedor vendedor{get;set;}
    public Articulo articulo{get;set;}

    public ICollection<IObservador> observadoresProducto {get; set;}

    public IDescuento descuentoAplicado {get; set;}


    public Producto(int precio_cents, int unidades, Vendedor vendedor, Articulo articulo){
        
        this.Precio_cents = precio_cents;
        this.Unidades = unidades;
        this.vendedor = vendedor;
        this.articulo = articulo;
        this.observadoresProducto = new List<IObservador>();
        descuentoAplicado = new SinDescuento();
    }

    public void InteresadosEnProducto(IObservador observador){
        observadoresProducto.Add(observador);
    }

    public void NOInteresadosEnProducto(IObservador observador){
        observadoresProducto.Remove(observador);
    }

    public void Notificar(){
        foreach (IObservador observador in observadoresProducto)
        {
            observador.Actualizar(this);
        }
    }

    public void VenderProducto(int CantidadVendida){

        this.Unidades -= CantidadVendida;

        if(this.Unidades < 5)
        {
            Notificar();
        }

    }

    public int AplicarDescuentoAProducto(){
        return descuentoAplicado.AplicarDescuento(this.Precio_cents);
    }

    // public string ObtenerInfoDescuento(){
    //     return descuentoAplicado.GetType().Name;
    // }

   } 
}
