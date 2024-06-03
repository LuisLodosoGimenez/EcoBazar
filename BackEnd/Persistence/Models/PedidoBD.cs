using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("Pedido")]
    public class PedidoBD : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }


        [Column("estado")]
        [required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Estado { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Column("id_comprador")]
        [required]
        public int IdComprador { get; set; }

    }
}