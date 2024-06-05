using backend.Mapper;
using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.MetodoFabrica
{
    public class VendedorFabrica : UsuarioFabrica
    {

        public Usuario CrearUsuario(Registro registro)
        {
            throw new Exception();
        }

        public async Task<Usuario> ObtenerUsuario(string nickName)
        {
            return await VendedorMapper.ObtenerVendedorPorNickName(nickName);
        }

        public async Task<Usuario> ObtenerUsuario(int idUsuario)
        {
            return await VendedorMapper.ObtenerVendedorPorId(idUsuario);
        }
    }
}