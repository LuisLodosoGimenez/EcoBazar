using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class VendedorMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static VendedorMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }



        public static Vendedor UsuarioBDAVendedor(UsuarioBD usuarioBD)
        {
            var vendedor = new Vendedor(usuarioBD.Nombre, usuarioBD.NickName, usuarioBD.Contraseña, usuarioBD.Email);
            vendedor.Id = usuarioBD.Id;
            vendedor.Edad = usuarioBD.Edad;
            vendedor.ImagenesUrl = usuarioBD.ImagenUrl;
            return vendedor;
        }
    }
}