namespace backend.Models{
    public class DescuentoOtonyo : IDescuento{
        public int AplicarDescuento(int PrecioInicialProducto){

            return (int)(PrecioInicialProducto * 0.8);

        }
    }
}
