using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("Producto")]
    public class ProductoBD : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }


        [Column("precio_cents")]
        [required]
        public int PrecioCents { get; set; }

        [Column("unidades")]
        [required]
        public int Unidades { get; set; }

        [Column("dias_entrega")]
        [required]
        public int DiasEntrega { get; set; }

        [Column("id_vendedor")]
        [required]
        public int IdVendedor { get; set; }

        [Column("id_articulo")]
        [required]
        public int IdArticulo { get; set; }

    }

    internal class requiredAttribute : Attribute
    {
    }
}
