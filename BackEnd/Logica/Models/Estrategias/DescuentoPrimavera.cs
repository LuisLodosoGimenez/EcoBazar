namespace backend.Models
{
    public class DescuentoPrimavera : IDescuento
    {
        public static string TextoDescuento = "DESCUENTO PRIMAVERA: 40%";
        public int CalcularDescuento(int PrecioInicialProducto)
        {

            return (int)(PrecioInicialProducto * 0.6);

        }
    }
}