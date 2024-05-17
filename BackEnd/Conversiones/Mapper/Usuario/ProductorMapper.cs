using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class ProductorMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static ProductorMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }


        public static Productor UsuarioBDAProductor(UsuarioBD usuarioBD)
        {
            var productor = new Productor(usuarioBD.Nombre, usuarioBD.Nick_name, usuarioBD.Contraseña, usuarioBD.Email);
            productor.Id = usuarioBD.Id;
            productor.Edad = usuarioBD.Edad;
            productor.ImagenesUrl = usuarioBD.ImagenUrl;
            return productor;
        }
    }

}