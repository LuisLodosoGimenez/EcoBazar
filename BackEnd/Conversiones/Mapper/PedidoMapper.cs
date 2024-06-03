using System.Collections.ObjectModel;
using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class PedidoMapper
    {
        public static async Task CrearPedido(int compradorId, int[] carritoCompra)
        {

            PedidoBD pedidoBD = new PedidoBD
            {
                Estado = "En proceso en vendedor",
                IdComprador = (int)compradorId,
            };

            var response = await SupabaseClientSingleton.getInstance()
                    .From<PedidoBD>()
                    .Insert(pedidoBD);
            Console.WriteLine("Pedido insertado correctamente en Supabase.");

            foreach (int idProducto in carritoCompra)
            {

                ProductoPedidoBD productoPedidoBD = new ProductoPedidoBD
                {
                    IdProducto = idProducto,
                    IdPedido = response.Model!.Id
                };

                await SupabaseClientSingleton.getInstance()
                    .From<ProductoPedidoBD>()
                    .Insert(productoPedidoBD);
                Console.WriteLine("ProductoPedido insertado correctamente en Supabase.");

            }
        }

        public static async Task<ICollection<Pedido>> ObtenerPedidosComprador(int compradorId)
        {

            var result = await SupabaseClientSingleton.getInstance()
                                .From<PedidoBD>()
                                .Where(x => x.IdComprador == compradorId)
                                .Get();


            return result.Models.ConvertAll<Pedido>(PedidoBDAPedido);

        }

        public static Pedido PedidoBDAPedido(PedidoBD pedidoBD)
        {

            ICollection<Producto> productosPedido = new Collection<Producto>();
            var result = SupabaseClientSingleton.getInstance()
                                .From<ProductoPedidoBD>()
                                .Where(x => x.IdPedido == pedidoBD.Id)
                                .Get();

            result.Wait();

            foreach (ProductoPedidoBD productoPedidoBD in result.Result.Models)
            {
                var prod = SupabaseClientSingleton.getInstance()
                                .From<ProductoBD>()
                                .Where(x => x.Id == productoPedidoBD.IdProducto)
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

