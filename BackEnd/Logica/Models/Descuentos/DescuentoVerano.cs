namespace backend.Models{
    public class DescuentoVerano : IDescuento{
        public int AplicarDescuento(int PrecioInicialProducto){

            return (int)(PrecioInicialProducto * 0.7);

        }
    }
}
