using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.MetodoFabrica
{

    public abstract class UsuarioFabrica
    {
        public UsuarioFabrica() { }


        public abstract Usuario CrearUsuario(Registro registro);

        public abstract Task<Usuario> ObtenerUsuario(string nickName);

        public abstract Task<Usuario> ObtenerUsuario(int idUsuario);
    }
}