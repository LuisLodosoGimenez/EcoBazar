using backend.Models;

namespace backend.Logica{
    public interface ISujeto{
        void AñadirObservadoresALista(IObservador observador);
        void BorrarObservadoresALista(IObservador observador);
        void Notificar();
    }
}