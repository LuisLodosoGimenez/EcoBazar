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

namespace backend.Services
{
    public interface Interfaz
    {
        Task<int> InsertarUser(UsuarioBD nuevouser);
        Task InsertarCompradorLuis(CompradorBD comprador);
        Task<bool> UsuarioExistePorApodo(string apodo);
        Task<UsuarioBD> UserByNick(string filtro);
        Task<List<UsuarioBD>> GetAllUsers();
        Task<List<ProductoBD>> GetAllProducts();
        Task<List<ArticuloBD>> GetAllArticles();
        Task InsertarCarrito(CarritoCompraBD nuevocarrito);
        Task<List<ArticuloBD>> ObtenerArticulosPorCategoria(string categoria);
        Task<List<ProductoBD>> ObtenerProductosPorID(int id);

        //De momento no sirven
        Task InsertarProducto(ProductoBD nuevoProducto);
        Task<List<ProductoBD>> GetProductsById(int y);
        Task EliminarProducto(ProductoBD producto);
        Task<UsuarioBD> UpdateAgeUser(UsuarioBD usuario,int edad1 ,int edad);
        Task<UsuarioBD> UserByAge(int filtro);
        Task<ProductoBD> ProductByPrice(int filtro);
        Task<List<CarritoCompraBD>> GetChart();
        Task<List<CompradorBD>> GetAllBuyers();

    }
}