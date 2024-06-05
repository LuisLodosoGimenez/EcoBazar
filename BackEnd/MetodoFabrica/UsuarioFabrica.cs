using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.MetodoFabrica
{

    public interface UsuarioFabrica
    {
        public Usuario CrearUsuario(Registro registro);

        public Task<Usuario> ObtenerUsuario(string nickName);

        public Task<Usuario> ObtenerUsuario(int idUsuario);
    }
}