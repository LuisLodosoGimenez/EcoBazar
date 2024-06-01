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
        Task RegistrarComprador(Comprador comprador);

        Task<object> IniciarSesion(string nickName, string contraseña);

        Task<IList<Producto>> ObtenerProductosPorCategoria(string categoria);

        Task<Comprador> AñadirProductoACarritoCompra(int idComprador, int idProducto);

        Task<Comprador> EliminarProductoEnCarritoCompra(int idComprador, int idProducto);

    }
}