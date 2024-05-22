using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("ProductoPedido")]
    public class ProductoPedidoBD : BaseModel
    {

        [PrimaryKey("id_pedido")]
        [Column("id_pedido")]
        public int IdPedido { get; set; }

        [PrimaryKey("id_producto")]
        [Column("id_producto")]
        public int idProducto { get; set; }

    }
}