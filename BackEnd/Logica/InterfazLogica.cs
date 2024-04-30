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
using backend.Services;
using backend.Models;

namespace backend.Logica
{
    public interface InterfazLogica
    {
        Usuario UserLogged();
        void Logout();
        void AñadirComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limiteGasto);
        void AddMember(Usuario user);
        Task Login(String nick, String password);
        IList<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuarioPorNick(string nick);
        IList<Producto> ObtenerProductos();
        IList<Articulo> ObtenerArticulos();
        IList<Articulo> GetArticlesByName(string keyWords);
        IList<CarritoCompra> GetChartByUser(Usuario user);
        IList<Producto> GetProductByChart(CarritoCompra carr);
         IList<Articulo> GetArticleByProduct(Producto prod);
        void AgregarAlCarrito(int usuarioId, int productoId);

        
        

        //CONSULTAR SI SIRVEN Y TAL
        IList<CarritoCompra> ObtenerChart();
        IList<Producto> GetContentsByParameters2(int keyWords);
        Usuario UpdateEdadUsuario(Usuario usuario,int edad);
        Producto ObtenerProductoPorPrecio(int precio);
    }
}