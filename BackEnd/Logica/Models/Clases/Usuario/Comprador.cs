using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario
    {

        public int? LimiteGastoCentsMes { get; set; }
        public ICollection<Producto> CarritoCompra { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }



        public Comprador(string nombre, string nickName, string contraseña, string email) : base(nombre, nickName, contraseña, email)
        {

            this.CarritoCompra = new Collection<Producto>();
            this.Pedidos = new Collection<Pedido>();
        }

        public void Actualizar(Producto productoBajoEnExistencias)
        {
            // MODIFICAR ESTADO DE COMRPRADOR
        }


        public InstantaneaCarrito GuardarCarrito()
        {
            return new InstantaneaCarrito(new Collection<Producto>((Collection<Producto>)CarritoCompra));
        }

        public void RecuperarCarrito(InstantaneaCarrito instantaneaCarrito)
        {
            CarritoCompra = instantaneaCarrito.carritoAnterior;
        }

        public class InstantaneaCarrito
        {
            public ICollection<Producto> carritoAnterior { get; set; }

            public InstantaneaCarrito(Collection<Producto> carritoActual)
            {
                carritoAnterior = new Collection<Producto>(carritoActual);
            }


            public InstantaneaCarrito ObtenerInstantaneaCarrito()
            {
                return this;
            }

        }
    }
}