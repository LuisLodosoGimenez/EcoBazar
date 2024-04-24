namespace backend.MetodoFabrica
{
    public class SecureFactory : Fabrica
    {

        public override UsuarioFabrica CrearUsuarioFabrica(string tipo)
        {
            if (tipo == "comprador")
            {
                return new UsuarioComprador();
            }
            else if (tipo == "vendedor"){
                return new UsuarioVendedor();
            }
            else
            {
                return null;
            }
        }
    }
}