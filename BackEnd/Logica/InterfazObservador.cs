using backend.Models;

namespace backend.Logica{
    public interface IObservador{
        string Actualizar(Producto productoBajoEnExistencias);
    }
}