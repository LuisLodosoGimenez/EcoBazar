
using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class ArticuloMapper
    {


        public async static Task<Articulo> ObtenerArticuloPorId(int articuloId)
        {

            var articulo = await SupabaseClientSingleton.getInstance()
                            .From<ArticuloBD>()
                            .Where(x => x.Id == articuloId)
                            .Get();

            return ConvertirArticuloBDAArticulo(articulo.Model!);

        }

        public async static Task<List<Articulo>> ObtenerArticulosPorCategoria(string categoria)
        {
            var result = await SupabaseClientSingleton.getInstance()
                    .From<ArticuloBD>()
                    .Where(c => c.Categoria == categoria)
                    .Get();

            return result.Models.ConvertAll<Articulo>(ConvertirArticuloBDAArticulo);
        }


        private static Articulo ConvertirArticuloBDAArticulo(ArticuloBD articuloBD)
        {
            var usu = UsuarioMapper.ObtenerUsuarioPorId(articuloBD.IdProductor);
            usu.Wait();
            Productor? productor = usu.Result as Productor;

            var imagenes = ImagenArticuloMapper.ObtenerImagenesArticuloPorId(articuloBD.Id);
            imagenes.Wait();
            List<string> imagenesArticulo = imagenes.Result as List<string>;

            return ArticuloBDAArticulo(articuloBD, productor!, imagenesArticulo);
        }

        public static Articulo ArticuloBDAArticulo(ArticuloBD articuloBD, Productor productor, List<string> imagenesArticulo)
        {
            Articulo articulo = new Articulo(articuloBD.Nombre, articuloBD.Categoria, articuloBD.EdadMin,
            articuloBD.ConsejosUtilizacion, articuloBD.ConsejosRetirada, articuloBD.Origen, articuloBD.ProcesoProduccion,
            articuloBD.ImpactoAmbientalSocial, articuloBD.ContribucionOds, productor);
            articulo.Id = articuloBD.Id;
            articulo.ImagenesUrl = imagenesArticulo;
            return articulo;
        }
    }
}