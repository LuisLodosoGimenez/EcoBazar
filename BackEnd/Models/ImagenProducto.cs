
namespace backend.Models{

    public class ImagenProducto{

        private int Id;
        private string Hash;
        private string Url;
        private Articulo Articulo;

        public ImagenProducto(int id, string hash, string url, Articulo articulo){
            this.Id = id;
            this.Hash = hash;
            this.Url = url;
            this.Articulo = articulo;
        }

        public int getId(){
            return this.Id;
        }

        public string getURL(){
            return this.Url;
        }

    }

}