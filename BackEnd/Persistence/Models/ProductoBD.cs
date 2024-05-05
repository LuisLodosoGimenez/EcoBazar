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
        public  int Precio_cents { get; set; }

        [Column("unidades")]
        [required]
        public int Unidades { get; set; }

        [Column("id_vendedor")]
        [required]
        public int Id_vendedor { get; set; }

        [Column("id_articulo")]
        [required]
        public int Id_articulo { get; set; }

    }

    internal class requiredAttribute : Attribute
    {
    }
}
