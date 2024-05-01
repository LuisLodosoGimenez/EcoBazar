using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    public class Comprador : Usuario{

        private int Limite_gasto_cents_mes;

        public Comprador(string nombre, string nick_name, string contraseña, string email, int edad, int limite) : base( nombre, nick_name, contraseña, email, edad){
            
            this.Limite_gasto_cents_mes = limite;
        }
    }
}