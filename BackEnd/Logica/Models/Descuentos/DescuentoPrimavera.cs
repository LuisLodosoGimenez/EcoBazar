namespace backend.Models{
    public class DescuentoPrimavera : IDescuento{
        public int AplicarDescuento(int PrecioInicialProducto){

            return (int)(PrecioInicialProducto * 0.6);

        }
    }
}
