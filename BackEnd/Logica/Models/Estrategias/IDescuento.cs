namespace backend.Models
{
    public interface IDescuento
    {
        public static string? TextoDescuento;
        int CalcularDescuento(int PrecioInicialProducto);
    }
}