using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class ProductoMapper
    {
        public async static Task<List<Producto>> ObtenerProductosPorIDArticulo(int idArticulo, Articulo? articulo)
        {
            var result = await SupabaseClientSingleton.getInstance()
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

            Producto producto = new Producto(productoBD.PrecioCents, productoBD.Unidades, productoBD.DiasEntrega, vendedor, articulo);
            producto.Id = productoBD.Id;
            return producto;


        }

    }
}