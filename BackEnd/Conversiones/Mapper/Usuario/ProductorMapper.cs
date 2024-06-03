using backend.MetodoFabrica;
using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class ProductorMapper
    {

        public async static Task<Productor> ObtenerProductorPorNickName(string nickName)
        {
            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            var productor = await SupabaseClientSingleton.getInstance()
                                    .From<ProductorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (productor.Models.Any()) return UsuarioBDAProductor(usuarioBD);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente NickName: " + nickName);

        }

        public async static Task<Productor> ObtenerProductorPorId(int idUsuario)
        {
            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.Id == idUsuario)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El ID '" + idUsuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            var productor = await SupabaseClientSingleton.getInstance()
                                    .From<ProductorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (productor.Models.Any()) return ProductorMapper.UsuarioBDAProductor(usuarioBD);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente ID: " + idUsuario);
        }


        public static Productor UsuarioBDAProductor(UsuarioBD usuarioBD)
        {
            var productor = new Productor(usuarioBD.Nombre, usuarioBD.NickName, usuarioBD.Contraseña, usuarioBD.Email);
            productor.Id = usuarioBD.Id;
            productor.Edad = usuarioBD.Edad;
            productor.ImagenesUrl = usuarioBD.ImagenUrl;
            return productor;
        }
    }

}