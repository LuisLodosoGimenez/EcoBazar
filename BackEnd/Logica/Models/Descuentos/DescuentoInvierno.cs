namespace backend.Models{
    public class DescuentoInvierno : IDescuento{
        public int AplicarDescuento(int PrecioInicialProducto){

            return (int)(PrecioInicialProducto * 0.9);

        }
    }
}
