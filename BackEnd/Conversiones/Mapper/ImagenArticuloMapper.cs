using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class ImagenArticuloMapper
    {
        public async static Task<List<string>> ObtenerImagenesArticuloPorId(int idArticulo)
        {
            var result = await SupabaseClientSingleton.getInstance()
                    .From<ImagenArticuloBD>()
                    .Where(x => x.IdArticulo == idArticulo)
                    .Get();

            return result.Models.ConvertAll<string>(ImagenArticuloBDAImagen);
        }

        public static string ImagenArticuloBDAImagen(ImagenArticuloBD imagenArticuloBD)
        {
            return imagenArticuloBD.Url;
        }
    }
}