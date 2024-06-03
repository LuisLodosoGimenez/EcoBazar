using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class UsuarioMapper
    {

        public async static Task<int> AñadirUsuario(UsuarioBD nuevouser)
        {
            var result = await SupabaseClientSingleton.getInstance()
                            .From<UsuarioBD>()
                            .Insert(nuevouser);
            Console.WriteLine("User insertado correctamente en Supabase.");
            return (int)result.Model!.Id!;
        }

        public async static Task<bool> ExisteNickNameEnUsuario(string nickName)
        {
            var result = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();

            return result.Models.Any();
        }

        public async static Task<bool> ExisteEmailEnUsuario(string email)
        {

            var result = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.Email == email)
                                .Get();

            return result.Models.Any();
        }


        public async static Task<System.Object> ObtenerUsuarioPorNickName(string nickName)
        {
            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            return await UsuarioMapper.ObtenerTipoUsuarioPorUsurioBD(usuarioBD);

        }


        public async static Task<Object> ObtenerUsuarioPorId(int id_usuario)
        {

            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.Id == id_usuario)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El ID '" + id_usuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            return await UsuarioMapper.ObtenerTipoUsuarioPorUsurioBD(usuarioBD);

        }


        private async static Task<System.Object> ObtenerTipoUsuarioPorUsurioBD(UsuarioBD usuarioBD)
        {

            try
            {
                var comprador = await SupabaseClientSingleton.getInstance()
                                    .From<CompradorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

                if (comprador.Models.Any()) return CompradorMapper.UsuarioBDYCompradorBDAComprador(usuarioBD, comprador.Model!);

                var vendedor = await SupabaseClientSingleton.getInstance()
                                    .From<VendedorBD>()
                                    .Where(x => x.Id == usuarioBD.Id)
                                    .Get();

                if (vendedor.Models.Any()) return VendedorMapper.UsuarioBDAVendedor(usuarioBD);

                var productor = await SupabaseClientSingleton.getInstance()
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