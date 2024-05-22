using System.Collections.ObjectModel;
using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class PedidoMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static PedidoMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }

        public static async Task CrearPedido(int compradorId, ICollection<Producto> carritoCompra)
        {

            PedidoBD pedidoBD = new PedidoBD
            {
                Estado = "En proceso en vendedor",
                idComprador = (int)compradorId,
            };

            var response = await _supabaseClient
                    .From<PedidoBD>()
                    .Insert(pedidoBD);
            Console.WriteLine("Pedido insertado correctamente en Supabase.");

            foreach (Producto producto in carritoCompra)
            {

                ProductoPedidoBD productoPedidoBD = new ProductoPedidoBD
                {
                    idProducto = (int)producto.Id!,
                    IdPedido = response.Model!.Id
                };

                await _supabaseClient
                    .From<ProductoPedidoBD>()
                    .Insert(productoPedidoBD);
                Console.WriteLine("ProductoPedido insertado correctamente en Supabase.");

            }
        }

        public static async Task<ICollection<Pedido>> ObtenerPedidosComprador(int compradorId)
        {

            var result = await _supabaseClient
                                .From<PedidoBD>()
                                .Where(x => x.idComprador == compradorId)
                                .Get();


            return result.Models.ConvertAll<Pedido>(PedidoBDAPedido);

        }

        public static Pedido PedidoBDAPedido(PedidoBD pedidoBD)
        {

            ICollection<Producto> productosPedido = new Collection<Producto>();
            var result = _supabaseClient
                                .From<ProductoPedidoBD>()
                                .Where(x => x.IdPedido == pedidoBD.Id)
                                .Get();

            result.Wait();

            foreach (ProductoPedidoBD productoPedidoBD in result.Result.Models)
            {
                var prod = _supabaseClient
                                .From<ProductoBD>()
                                .Where(x => x.Id == productoPedidoBD.idProducto)
                                .Get();
                result.Wait();
                productosPedido.Add(ProductoMapper.ConvertirProductoBDAProducto(prod.Result.Model!));

            }
            Pedido pedido = new Pedido(pedidoBD.Id, pedidoBD.Estado);
            pedido.ProductosPedido = productosPedido;
            return pedido;
        }
    }
}

