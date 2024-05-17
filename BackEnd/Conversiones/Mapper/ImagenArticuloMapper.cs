using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class ImagenArticuloMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static ImagenArticuloMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }


        public async static Task<List<string>> ObtenerImagenesArticuloPorId(int idArticulo)
        {
            var result = await _supabaseClient
                    .From<ImagenArticuloBD>()
                    .Where(x => x.idArticulo == idArticulo)
                    .Get();

            return result.Models.ConvertAll<string>(ImagenArticuloBDAImagen);
        }

        public static string ImagenArticuloBDAImagen(ImagenArticuloBD imagenArticuloBD)
        {
            return imagenArticuloBD.url;
        }
    }
}