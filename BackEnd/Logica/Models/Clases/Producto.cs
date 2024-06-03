using backend.Logica;

namespace backend.Models
{
    public class Producto
    {

        public int? Id { get; set; }
        public int PrecioCents { get; set; }
        public int Unidades { get; set; }
        public int DiasEntrega { get; set; }
        public Vendedor Vendedor { get; set; }
        public Articulo Articulo { get; set; }

        public ICollection<IObservador> ObservadoresProducto { get; set; }

        public IDescuento? DescuentoAplicado { get; set; }

        public Producto(int precioCents, int unidades, int diasEntrega, Vendedor vendedor, Articulo articulo)
        {

            this.PrecioCents = precioCents;
            this.Unidades = unidades;
            this.Vendedor = vendedor;
            this.Articulo = articulo;
            this.DiasEntrega = diasEntrega;
            this.ObservadoresProducto = new List<IObservador>();
        }

        public void AñadirObservadoresALista(IObservador observador)
        {
            ObservadoresProducto.Add(observador);
        }

        public void BorrarObservadoresDeLista(IObservador observador)
        {
            ObservadoresProducto.Remove(observador);
        }

        public void Notificar()
        {
            foreach (IObservador observador in ObservadoresProducto)
            {
                observador.Actualizar(this);
            }
        }

        public void VenderProducto(int CantidadVendida)
        {

            this.Unidades -= CantidadVendida;

            if (this.Unidades < 5)
            {
                Notificar();
            }

        }

        public void AplicarDescuento()
        {
            if (this.DescuentoAplicado != null)
            {
                this.PrecioCents = DescuentoAplicado.CalcularDescuento(this.PrecioCents);
            }

        }
    }
}
