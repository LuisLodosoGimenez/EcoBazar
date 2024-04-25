using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.Models
{
    [Table("Comprador")]
    public class Comprador : Usuario
    {
        [PrimaryKey]
        [Column("id_usuario")]
        public int Id_usuario { get; set; }

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }

    }
}