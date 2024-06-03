namespace backend.Models
{
    public class DescuentoVerano : IDescuento
    {
        public string TextoDescuento { get; set; } = "DESCUENTO VERANO: 30%";
        public int CalcularDescuento(int PrecioInicialProducto)
        {
            return (int)(PrecioInicialProducto * 0.7);
        }
    }
}