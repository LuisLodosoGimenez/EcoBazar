using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("ImagenProducto")]
    public class ImagenProductoBD : BaseModel
    {

        [PrimaryKey]
        [Column("id")]
        public int Id { get; set; }

        [Column("hash")]
        public string Hash { get; set; }

        [Column("url")]
        public string Url { get; set; }

        [Column("id_articulo")]
        public int Id_articulo { get; set; }


    }
}