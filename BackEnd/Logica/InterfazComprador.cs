using backend.Models;

namespace backend.Logica{
    public interface IComprador{
        string Actualizar(Producto productoBajoEnExistencias);
    }
}