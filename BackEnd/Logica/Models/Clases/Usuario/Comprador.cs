using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using backend.Logica;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario, IObservador
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

            // CUANDO SE LLAME A ESTE MÉTODO ACTUALIZAR SE PODRÍAN REALIZAR DIVERSAS FUNCIONES:
            //
            //  - MODIFICAR EL ESTADO DEL COMRPRADOR SEGÚN EL ESTADO DEL PRODUCTO BAJO EN EXISTENCIAS 
            //    QUE OBTENEMOS CÓMO PARÁMETRO
            // 
            //  - O POR EJEMPLO, MANDAR UN EMAIL UTILIZANDO EL CORREO ELECTRÓNICO DEL COMPRADOR
            //    INDICANDO EL NÚMERO DE PRODUCTOS QUE QUEDAN (OBTENIÉNDOLO DE "productoBajoEnExistencias")

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