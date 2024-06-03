using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class VendedorMapper
    {


        public async static Task<Vendedor> ObtenerVendedorPorNickName(string nickName)
        {
            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            var vendedor = await SupabaseClientSingleton.getInstance()
                                    .From<VendedorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (vendedor.Models.Any()) return UsuarioBDAVendedor(usuarioBD);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente NickName: " + nickName);
        }

        public async static Task<Vendedor> ObtenerVendedorPorId(int idUsuario)
        {

            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.Id == idUsuario)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El ID '" + idUsuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            var vendedor = await SupabaseClientSingleton.getInstance()
                                    .From<VendedorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (vendedor.Models.Any()) return VendedorMapper.UsuarioBDAVendedor(usuarioBD);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente ID: " + idUsuario);

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