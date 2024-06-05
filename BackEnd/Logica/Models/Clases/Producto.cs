using backend.Logica;

namespace backend.Models
{
    public class Producto : ISujeto
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

            if (vendedor == null) throw new Exception("El parámentro vendedor no puede quedar a NULL");
            else if (articulo == null) throw new Exception("El parámentro articulo no puede quedar a NULL");

            this.PrecioCents = precioCents;
            this.Unidades = unidades;
            this.Vendedor = vendedor;
            this.Articulo = articulo;
            this.DiasEntrega = diasEntrega;
            this.ObservadoresProducto = new List<IObservador>();
        }

        public void AñadirObservador(IObservador observador)
        {
            ObservadoresProducto.Add(observador);
        }

        public void EliminarObservador(IObservador observador)
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
