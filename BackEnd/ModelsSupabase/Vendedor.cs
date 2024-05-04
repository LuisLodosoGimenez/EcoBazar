using Postgrest.Attributes;
using Postgrest.Models;


namespace backend.ModelsSupabase
{
    [Table("Vendedor")]
    public class VendedorBD : UsuarioBD
    {

        public string getNombre(){
            return this.Nombre!;
        }
    }
}