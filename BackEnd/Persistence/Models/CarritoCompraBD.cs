using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("CarritoCompra")]
    public class CarritoCompraBD : BaseModel
    {

        [PrimaryKey("id_comprador")]
        [required]
        public int Id_comprador { get; set; }

        [PrimaryKey("id_producto")]
        [required]
        public int Id_producto { get; set; }

    }
}