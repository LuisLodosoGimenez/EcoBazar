namespace backend.MetodoFabrica
{

    public abstract class Fabrica
    {
        public Fabrica(){}
        public abstract UsuarioFabrica CrearUsuarioFabrica(string tipo);
    }
}