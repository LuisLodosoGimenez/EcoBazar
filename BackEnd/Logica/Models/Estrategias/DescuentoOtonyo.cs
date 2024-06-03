namespace backend.Models
{
    public class DescuentoOtonyo : IDescuento
    {
        public string TextoDescuento { get; set; } = "DESCUENTO OTOÑO: 20%";
        public int CalcularDescuento(int PrecioInicialProducto)
        {

            return (int)(PrecioInicialProducto * 0.8);

        }
    }
}