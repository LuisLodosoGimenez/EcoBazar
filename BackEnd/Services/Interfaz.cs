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
        Task InsertarProducto(Producto nuevoProducto);
        Task<List<Producto>> GetProductsById(int y);
        Task EliminarProducto(Producto producto);
        Task<List<Producto>> GetAllProducts();
        Task<List<Usuario>> GetAllUsers();
        Task<Usuario> UserByNick(string filtro);
        Task InsertarUser(Usuario nuevouser);
        Task<bool> UsuarioExistePorApodo(string apodo);
        Task<Usuario> UpdateAgeUser(Usuario usuario,int edad1 ,int edad);
        Task<Usuario> UserByAge(int filtro);
        Task InsertarCarrito(CarritoCompra nuevocarrito);
        //Task<Comprador> BuyerByNick(string filtro);
        Task<Producto> ProductByPrice(int filtro);
        Task InsertarBuyer(Comprador3 nuevobuyer);
        Task<List<CarritoCompra>> GetChart();
        Task<List<Articulo>> GetAllArticles();
        Task Insert1<Comprador>(Comprador item) where Comprador : Usuario,new();
        Task InsertarBuyerEnUsuarios(Comprador2 nuevouser);



        //   #######################################  LUIS  ########################################################

        Task InsertarCompradorLuis(Compradorluis comprador);
        //   ########################################################################################################

    }
}