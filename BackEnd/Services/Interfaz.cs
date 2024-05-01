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
using backend.Models;
using Postgrest.Models;

namespace backend.Services
{
    public interface Interfaz
    {
        Task InsertarUser(Usuario nuevouser);
        Task InsertarCompradorLuis(Comprador comprador);
        Task<bool> UsuarioExistePorApodo(string apodo);
        Task<Usuario> UserByNick(string filtro);
        Task<List<Usuario>> GetAllUsers();
        Task<List<Producto>> GetAllProducts();
        Task<List<Articulo>> GetAllArticles();
        Task InsertarCarrito(CarritoCompra nuevocarrito);
        Task<List<Articulo>> ObtenerArticulosPorCategoria(string categoria);
        Task<List<Producto>> ObtenerProductosPorID(int id);

        //De momento no sirven
        Task InsertarProducto(Producto nuevoProducto);
        Task<List<Producto>> GetProductsById(int y);
        Task EliminarProducto(Producto producto);
        Task<Usuario> UpdateAgeUser(Usuario usuario,int edad1 ,int edad);
        Task<Usuario> UserByAge(int filtro);
        Task<Producto> ProductByPrice(int filtro);
        Task<List<CarritoCompra>> GetChart();
        Task<List<Comprador>> GetAllBuyers();

    }
}