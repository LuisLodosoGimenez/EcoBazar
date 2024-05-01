using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("Comprador")]
    public class CompradorBD : BaseModel
    {

        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }

        [Column("limite_gasto_cents_mes")]
        public int Limite_gasto_cents_mes { get; set; }
    }
}