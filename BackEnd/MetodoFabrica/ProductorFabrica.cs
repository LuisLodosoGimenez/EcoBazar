using backend.Mapper;
using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.MetodoFabrica
{
    public class ProductorFabrica : UsuarioFabrica
    {

        public Usuario CrearUsuario(Registro registro)
        {
            Productor productor = new Productor(registro.Nombre!, registro.NickName!, registro.Contraseña!, registro.Email!);
            productor.Edad = registro.Edad;
            return productor;
        }

        public async Task<Usuario> ObtenerUsuario(string nickName)
        {
            return await ProductorMapper.ObtenerProductorPorNickName(nickName);
        }

        public async Task<Usuario> ObtenerUsuario(int idUsuario)
        {
            return await ProductorMapper.ObtenerProductorPorId(idUsuario);
        }
    }
}