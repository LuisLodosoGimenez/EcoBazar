using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Postgrest.Models;

using backend.Models;
using backend.ModelsSupabase;

namespace backend.Conversiones{
     public interface IConversiones{
        UsuarioBD ConvertirUsuario(Usuario usuario);
        CompradorBD ConvertirComprador(Comprador comprador,int userId);
        Usuario ConvertirBDaUsuario(UsuarioBD usuarioBD);
        Producto ConvertirBDaProducto(ProductoBD productoBD);
        CarritoCompraBD ConvertirCarritoCompra(CarritoCompra carrito);
        List<Producto> ConvertirListaBDaProducto(List<ProductoBD> listaProductos);
        List<Usuario> ConvertirListaBDaUsuario(List<UsuarioBD> listaUsuario);
        List<Articulo> ConvertirListaBDaArticulo(List<ArticuloBD> listaArticulos);
        List<CarritoCompra> ConvertirListaBDaCarritoCompra(List<CarritoCompraBD> listaCarrito);
     }
}