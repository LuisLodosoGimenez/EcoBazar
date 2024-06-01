using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Mapper
{

    public static class UsuarioMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static UsuarioMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }



        public async static Task<int> AñadirUsuario(UsuarioBD nuevouser)
        {
            var result = await _supabaseClient
                            .From<UsuarioBD>()
                            .Insert(nuevouser);
            Console.WriteLine("User insertado correctamente en Supabase.");
            return (int)result.Model!.Id!;
        }

        public async static Task<bool> ExisteNickNameEnUsuario(string nickName)
        {
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();

            return result.Models.Any();
        }

        public async static Task<bool> ExisteEmailEnUsuario(string email)
        {

            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Email == email)
                                .Get();

            return result.Models.Any();
        }


        public async static Task<System.Object> ObtenerUsuarioPorNickName(string nickName)
        {
            var usuario = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            return await UsuarioMapper.ObtenerTipoUsuarioPorUsurioBD(usuarioBD);

        }


        public async static Task<Object> ObtenerUsuarioPorId(int idUsuario)
        {

            var usuario = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Id == idUsuario)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El ID '" + idUsuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            return await UsuarioMapper.ObtenerTipoUsuarioPorUsurioBD(usuarioBD);

        }


        private async static Task<System.Object> ObtenerTipoUsuarioPorUsurioBD(UsuarioBD usuarioBD)
        {

            try
            {
                var comprador = await _supabaseClient
                                    .From<CompradorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

                if (comprador.Models.Any()) return CompradorMapper.UsuarioBDYCompradorBDAComprador(usuarioBD, comprador.Model!);

                var vendedor = await _supabaseClient
                                    .From<VendedorBD>()
                                    .Where(x => x.Id == usuarioBD.Id)
                                    .Get();

                if (vendedor.Models.Any()) return VendedorMapper.UsuarioBDAVendedor(usuarioBD);

                var productor = await _supabaseClient
                                    .From<ProductorBD>()
                                    .Where(x => x.Id == usuarioBD.Id)
                                    .Get();

                if (productor.Models.Any()) return ProductorMapper.UsuarioBDAProductor(usuarioBD);

                throw new Exception("El usuario no corresponde con ningun comprador, vendedor o productor.");

            }

            //TODO: DELETE?
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }
        }

        public static Usuario UsuarioBDAUsuario(UsuarioBD usuarioBD)
        {

            var usuario = new Usuario(usuarioBD.Nombre, usuarioBD.NickName, usuarioBD.Contraseña, usuarioBD.Email);
            usuario.Edad = usuarioBD.Edad;
            return usuario;

        }



    }
}