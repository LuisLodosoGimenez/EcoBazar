using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("CarritoCompra")]
    public class CarritoCompraBD : BaseModel
    {

        [PrimaryKey]
        [Column("id_usuario")]
        public int Id_usuario { get; set; }
        
        [PrimaryKey]
        [Column("id_producto")]
        public int Id_producto { get; set; }

    }
}