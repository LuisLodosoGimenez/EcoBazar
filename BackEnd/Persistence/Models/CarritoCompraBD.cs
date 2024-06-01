using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("CarritoCompra")]
    public class CarritoCompraBD : BaseModel
    {

        [PrimaryKey("id_comprador")]
        [Column("id_comprador")]
        public int IdComprador { get; set; }

        [PrimaryKey("id_producto")]
        [Column("id_producto")]
        public int IdProducto { get; set; }

    }
}