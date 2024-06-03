using System.Collections.ObjectModel;
using backend.Logica;
using backend.Models;
using static backend.Models.Comprador;

namespace backend.Memento
{

    public class RegistroCarrito
    {

        ICollection<InstantaneaCarrito> CarritosAnteriores { get; set; }

        public RegistroCarrito()
        {
            this.CarritosAnteriores = new Collection<InstantaneaCarrito>();

        }

        public void AgregarRegistro(InstantaneaCarrito carritoAnterior)
        {
            CarritosAnteriores.Add(carritoAnterior);
        }

        public void mostrarListado()
        {

            if (CarritosAnteriores != null)
            {

                foreach (InstantaneaCarrito carrito in CarritosAnteriores)
                {
                    carrito.ObtenerInstantaneaCarrito();
                }
            }
            else
            {
                Console.WriteLine("Carrito vacío");
            }
        }

        public InstantaneaCarrito? Deshacer()
        {

            if (CarritosAnteriores.Count() > 0)
            {
                InstantaneaCarrito ultimo = CarritosAnteriores.Last();
                CarritosAnteriores.Remove(ultimo);
                return ultimo;
            }
            return null;
        }

    }

}