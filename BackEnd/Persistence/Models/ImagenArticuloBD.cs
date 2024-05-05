using Postgrest.Attributes;
using Postgrest.Models;

namespace backend.ModelsSupabase
{
    [Table("ImagenArticulo")]
    public class ImagenArticuloBD : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }


        [Column("url")]
        [required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string url { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [Column("id_articulo")]
        [required]
        public int idArticulo { get; set; }

    }
}