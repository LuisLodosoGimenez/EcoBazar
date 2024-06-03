namespace backend.Models
{
    public class DescuentoInvierno : IDescuento
    {

        public static string TextoDescuento = "DESCUENTO INVIERNO: 10%";

        public int CalcularDescuento(int PrecioInicialProducto)
        {

            return (int)(PrecioInicialProducto * 0.9);

        }
    }
}