using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class ProductoMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static ProductoMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }

        public async static Task<List<Producto>> ObtenerProductosPorIDArticulo(int idArticulo, Articulo? articulo)
        {
            var result = await _supabaseClient
                                .From<ProductoBD>()
                                .Where(a => a.IdArticulo == idArticulo)
                                .Get();



            return result.Models.ConvertAll(ConvertirProductoBDAProducto);
        }

        public static Producto ConvertirProductoBDAProducto(ProductoBD productoBD)
        {

            var vend = UsuarioMapper.ObtenerUsuarioPorId(productoBD.IdVendedor);
            vend.Wait();
            Vendedor vendedor = (vend.Result as Vendedor)!;

            var art = ArticuloMapper.ObtenerArticuloPorId(productoBD.IdArticulo);
            art.Wait();
            Articulo articulo = art.Result as Articulo;



            return ProductoBDAProducto(productoBD, vendedor!, articulo!);
        }


        public static Producto ProductoBDAProducto(ProductoBD productoBD, Vendedor vendedor, Articulo articulo)
        {

            Producto producto = new Producto(productoBD.PrecioCents, productoBD.Unidades, vendedor, articulo);
            producto.Id = productoBD.Id;
            return producto;


        }
    }
}