using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("Producto")]
    public class ProductoBD : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }


        [Column("precio_cents")]
        public int Precio_cents { get; set; }

        [Column("unidades")]
        public int Unidades { get; set; }

        [Column("id_articulo")]
        public int Id_articulo { get; set; }

        [Column("id_vendedor")]
        public int Id_usuario { get; set; }
        

    }
}
