using backend.Models;

namespace backend.MetodoFabrica
{

    public abstract class Fabrica
    {
        public Fabrica(){}
        public abstract Usuario CrearUsuarioFabrica(string tipo, string nombre, string nickname, string contraseña, string email);
    }
}