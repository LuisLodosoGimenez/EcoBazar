
using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class ArticuloMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static ArticuloMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }

        public async static Task<Articulo> ObtenerArticuloPorId(int articuloId)
        {

            var articulo = await _supabaseClient
                            .From<ArticuloBD>()
                            .Where(x => x.Id == articuloId)
                            .Get();

            return ConvertirArticuloBDAArticulo(articulo.Model!);

        }

        public async static Task<List<Articulo>> ObtenerArticulosPorCategoria(string categoria)
        {
            var result = await _supabaseClient
                    .From<ArticuloBD>()
                    .Where(c => c.Categoria == categoria)
                    .Get();

            return result.Models.ConvertAll<Articulo>(ConvertirArticuloBDAArticulo);
        }


        private static Articulo ConvertirArticuloBDAArticulo(ArticuloBD articuloBD)
        {
            var usuario = UsuarioMapper.ObtenerUsuarioPorId(articuloBD.IdProductor);
            usuario.Wait();
            Productor? productor = usuario.Result as Productor;

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