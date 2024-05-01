
namespace backend.Models
{
    public class CarritoCompra{
        private int Id_usuario;
        private int Id_producto;

        public CarritoCompra(int id_usuario, int id_producto){
            this.Id_usuario = id_usuario;
            this.Id_producto = id_producto;
        }

        public int getId_Usuario(){
            return this.Id_usuario;
        }

        public int getId_Producto(){
            return this.Id_producto;
        }
    }
}