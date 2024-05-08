using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario{

        public int? Limite_gasto_cents_mes{get;set;}
        public ICollection<Producto> CarritoCompra{get;set;}



            public Comprador(string nombre, string nick_name, string contraseña, string email) : base( nombre, nick_name, contraseña, email){
                
                this.CarritoCompra = new Collection<Producto>();
            }     

            public InstantaneaCarrito GuardarCarrito(){
                return new InstantaneaCarrito(new Collection<Producto>((Collection<Producto>)CarritoCompra));
            }

            public void RecuperarCarrito(InstantaneaCarrito instantaneaCarrito)
            {
                CarritoCompra = instantaneaCarrito.carritoAnterior;
            }

            public class InstantaneaCarrito{
                public ICollection<Producto> carritoAnterior {get;set;}

                public InstantaneaCarrito(Collection<Producto> carritoActual){
                    carritoAnterior = new Collection<Producto>(carritoActual);
                }

                public Collection<Producto> ObtenerCarritoAnterior()
                {
                    return (Collection<Producto>) this.carritoAnterior;
                }

                public InstantaneaCarrito GetInstantaneaCarrito(){
                    return this;
                }
            
        }  
    }

}