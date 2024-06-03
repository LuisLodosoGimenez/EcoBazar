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
using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.Logica
{
    public interface InterfazLogica
    {
        Task RegistrarComprador(Registro registro);

        Task<Usuario> IniciarSesion(string nickName, string contraseña, string tipoSesion);

        Task<IList<Producto>> ObtenerProductosPorCategoria(string categoria);

        Task<Comprador> AñadirProductoACarritoCompra(int idComprador, int idProducto);

        Task<Comprador> EliminarProductoEnCarritoCompra(int idComprador, int idProducto);

        Task<Comprador> CrearPedidoAComprador(CreacionPedidoPeticion creacionPedidoPeticion);

        ///################################################################

        // void AddMember(Usuario user);
        // Task Login(String nick, String password);
        //IList<Usuario> ObtenerUsuarios();

        // IList<Producto> ObtenerProductos();
        // IList<Articulo> ObtenerArticulos();
        // IList<Articulo> GetArticlesByName(string keyWords);
        // //IList<CarritoCompra> GetChartByUser(Usuario user);
        // //IList<Producto> GetProductByChart(CarritoCompra carr);
        //  IList<Articulo> GetArticleByProduct(Producto prod);
        // void AgregarAlCarrito(int usuarioId, int productoId);
        // IList<Producto> ObtenerProductosPorCategoria(string keyWords);
        // IList<Producto>  FiltrarArticulos(IList<Articulo> filtrados);



        // //CONSULTAR SI SIRVEN Y TAL
        // //IList<CarritoCompra> ObtenerChart();
        // Usuario UpdateEdadUsuario(Usuario usuario,int edad);
        // Producto ObtenerProductoPorPrecio(int precio);
    }
}