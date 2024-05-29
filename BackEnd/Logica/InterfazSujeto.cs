using backend.Models;

namespace backend.Logica{
    public interface ISujeto{
        void AñadirObservadoresALista(IObservador observador);
        void BorrarObservadoresDeLista(IObservador observador);
        void Notificar();
    }
}