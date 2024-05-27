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
                                .Where(a => a.Id_articulo == idArticulo)
                                .Get();



            return result.Models.ConvertAll(ConvertirProductoBDAProducto);
        }

        public static Producto ConvertirProductoBDAProducto(ProductoBD productoBD)
        {

            var vend = UsuarioMapper.ObtenerUsuarioPorId(productoBD.Id_vendedor);
            vend.Wait();
            Vendedor vendedor = (vend.Result as Vendedor)!;

            var art = ArticuloMapper.ObtenerArticuloPorId(productoBD.Id_articulo);
            art.Wait();
            Articulo articulo = art.Result as Articulo;



            return ProductoBDAProducto(productoBD, vendedor!, articulo!);
        }


        public static Producto ProductoBDAProducto(ProductoBD productoBD, Vendedor vendedor, Articulo articulo)
        {

            Producto producto = new Producto(productoBD.Precio_cents, productoBD.Unidades, vendedor, articulo);
            producto.Id = productoBD.Id;
            return ProductoConDescuento(producto);


        }

        public static Producto ProductoConDescuento(Producto producto){

            int mesActual = DateTime.Now.Month;

            switch(mesActual){
                case 1:
                    producto.descuentoAplicado = new DescuentoInvierno();
                    break;
               
                case 4:
                    producto.descuentoAplicado = new DescuentoPrimavera();
                    break;
               
                case 7:
                    producto.descuentoAplicado = new DescuentoVerano();
                    break;
                
                case 11:
                    producto.descuentoAplicado = new DescuentoOtonyo();
                    break;
            
            }
            return producto;
        }

    }
}