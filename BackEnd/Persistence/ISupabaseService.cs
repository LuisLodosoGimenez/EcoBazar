using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using backend.ModelsSupabase;
using Postgrest.Models;
using backend.Models;

namespace backend.Services
{
    public interface ISupabaseService
    {
        Task AñadirComprador(CompradorBD compradorBD);
        Task<int> AñadirUsuario(UsuarioBD nuevouser);
        Task<bool> ExisteNickNameEnUsuario(string nickName);
        Task<bool> ExisteEmailEnUsuario(string email);
        Task<System.Object> ObtenerUsuarioPorNickName(string nickName);

        Task<System.Object> ObtenerUsuarioPorId(int id_usuario);

        Task<ICollection<Producto>> ObtenerCarritoCompra(int compradorId);

        Task<Articulo> ObtenerArticuloPorId(int articuloId);



        //Task<List<UsuarioBD>> ObtenerTodosLosUsuarios();

        // Task<int> InsertarUser(UsuarioBD nuevouser);
        // Task InsertarCompradorLuis(CompradorBD comprador);
        // Task<bool> UsuarioExistePorApodo(string apodo);
        // Task<UsuarioBD> UserByNick(string filtro);

        // Task<List<ProductoBD>> GetAllProducts();
        // Task<List<ArticuloBD>> GetAllArticles();
        // Task InsertarCarrito(CarritoCompraBD nuevocarrito);
        // Task<List<ArticuloBD>> ObtenerArticulosPorCategoria(string categoria);
        // Task<List<ProductoBD>> ObtenerProductosPorID(int id);

        // //De momento no sirven
        // Task InsertarProducto(ProductoBD nuevoProducto);
        // Task<List<ProductoBD>> GetProductsById(int y);
        // Task EliminarProducto(ProductoBD producto);
        // Task<UsuarioBD> UpdateAgeUser(UsuarioBD usuario,int edad1 ,int edad);
        // Task<UsuarioBD> UserByAge(int filtro);
        // Task<ProductoBD> ProductByPrice(int filtro);
        // Task<List<CarritoCompraBD>> GetChart();
        // Task<List<CompradorBD>> GetAllBuyers();

    }
}