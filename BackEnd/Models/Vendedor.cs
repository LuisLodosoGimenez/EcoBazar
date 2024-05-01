namespace backend.Models
{
    public class Vendedor : Usuario{

        public Vendedor(string nombre, string nick_name, string contraseña, string email, int edad) : base( nombre, nick_name, contraseña, email, edad){
         
        }
    }
}