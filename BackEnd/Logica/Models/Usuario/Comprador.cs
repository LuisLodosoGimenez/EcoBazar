using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using backend.Logica;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario, IObservador{

        public int? Limite_gasto_cents_mes{get;set;}
        public ICollection<Producto> CarritoCompra{get;set;}



        public Comprador(string nombre, string nick_name, string contraseña, string email) : base( nombre, nick_name, contraseña, email){
            
            this.CarritoCompra = new Collection<Producto>();
        }        

        public string Actualizar(Producto productoBajoEnExistencias){

            return "El producto " + productoBajoEnExistencias.articulo.Nombre + " está a punto de agotarse";
        }

    }
}