using backend.Models;

namespace backend.Logica
{
    public interface IObservador
    {
        void Actualizar(Producto productoBajoEnExistencias);
    }
}