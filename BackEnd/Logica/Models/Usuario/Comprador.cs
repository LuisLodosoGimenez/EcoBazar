using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario
    {

        public int? Limite_gasto_cents_mes { get; set; }
        public ICollection<Producto> CarritoCompra { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }



        public Comprador(string nombre, string nick_name, string contraseña, string email) : base(nombre, nick_name, contraseña, email)
        {

            this.CarritoCompra = new Collection<Producto>();
            this.Pedidos = new Collection<Pedido>();
        }
    }
}