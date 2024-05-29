using backend.Models;

namespace backend.MetodoFabrica
{
    public class SecureFactory : Fabrica
    {

        public override Usuario CrearUsuarioFabrica(string tipo, string nombre, string nickname, string contraseña, string email)
        {
            if (tipo == "comprador")
            {
                return new Comprador(nombre, nickname, contraseña, email);
            }
            else if (tipo == "vendedor"){
                return new Vendedor(nombre, nickname, contraseña, email);
            }
            else
            {
                return new Usuario(nombre, nickname, contraseña, email);
            }
        }
    }
}