using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Pedido
    {

        public int Id { get; set; }
        public string Estado { get; set; }
        public ICollection<Producto> ProductosPedido { get; set; }



        public Pedido(int id, string estado)
        {

            this.Id = id;
            this.Estado = estado;
            ProductosPedido = new Collection<Producto>();
        }
    }
}