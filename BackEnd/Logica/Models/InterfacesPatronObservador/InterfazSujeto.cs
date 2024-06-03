using backend.Models;

namespace backend.Logica
{
    public interface ISujeto
    {
        void AñadirObservador(IObservador observador);
        void EliminarObservador(IObservador observador);
        void Notificar();
    }
}