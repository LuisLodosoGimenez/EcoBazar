using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]



    public class ApiController : ControllerBase
    {
        // private static ApiController _instancia;
        private readonly InterfazLogica _logica;

        public ApiController(InterfazLogica logica)
        {
            this._logica = logica;
        }

        // public static ApiController GetInstance(InterfazLogica logica){
        //     if(_instancia == null)
        //         _instancia = new ApiController(logica);

        //     return _instancia;
        // }





        // ----------------------------------------------- //
        public class Registro
        {
            public string? Nombre { get; set; }
            public string? NickName { get; set; }
            public string? Contraseña { get; set; }
            public string? Email { get; set; }
            public int? Edad { get; set; }
            public int? LimiteGasto { get; set; }
        }
        [HttpPost("Registrarse")]
        public async Task<IActionResult> RegistrarCompradorAsync(Registro registro)
        {

            try
            {
                var comprador = new Comprador(registro.Nombre!, registro.NickName!, registro.Contraseña!, registro.Email!);
                comprador.Edad = registro.Edad;
                comprador.LimiteGastoMes = registro.LimiteGasto;
                await _logica.RegistrarComprador(comprador);

            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    response = ex.Message,
                };
                return BadRequest(errorResponse);
            }

            return Ok();

        }
        // ----------------------------------------------- //






        // ----------------------------------------------- //
        public class InicioSesion
        {
            public string? NickName { get; set; }
            public string? Contraseña { get; set; }
        }
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(InicioSesion inicioSesion)
        {
            try
            {
                var perfil = await _logica.IniciarSesion(inicioSesion.NickName!, inicioSesion.Contraseña!);

                if (perfil.GetType() == Type.GetType("backend.Models.Comprador"))
                {

                    Comprador? comprador = perfil as Comprador;

                    var responseData = new
                    {
                        Comprador = comprador,
                    };
                    return Ok(responseData);

                }
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    response = ex.Message,
                };
                return BadRequest(errorResponse);
            }

            return BadRequest("No se trata de un comprador");
        }
        // ----------------------------------------------- //







        // ----------------------------------------------- //
        [HttpGet("Categoria")]
        public async Task<IActionResult> ObtenerProductorPorCategoría(string categoria)
        {

            try
            {
                var listaProductos = await _logica.ObtenerProductosPorCategoria(categoria);
                return Ok(listaProductos);

            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    response = ex.Message,
                };
                return BadRequest(errorResponse);
            }
        }
        // ----------------------------------------------- //







        // ----------------------------------------------- //
        public class CarritoCompra
        {
            public int? idComprador { get; set; }
            public int? idProducto { get; set; }
        }
        [HttpPost("AñadirProductoACarritoCompra")]
        public async Task<IActionResult> AñadirProductoACarritoCompra(CarritoCompra carritoCompra)
        {
            try
            {
                var perfil = await _logica.AñadirProductoACarritoCompra((int)carritoCompra.idComprador!, (int)carritoCompra.idProducto!);

                if (perfil.GetType() == Type.GetType("backend.Models.Comprador"))
                {

                    Comprador? comprador = perfil as Comprador;

                    var responseData = new
                    {
                        CarritoCompra = comprador.CarritoCompra,
                    };
                    return Ok(responseData);

                }
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    response = ex.Message,
                };
                return BadRequest(errorResponse);
            }
            return BadRequest("No se trata de un comprador");
        }


        // ------- //


        [HttpPost("EliminarProductoEnCarritoCompra")]
        public async Task<IActionResult> EliminarProductoEnCarritoCompra(CarritoCompra carritoCompra)
        {
            try
            {
                var perfil = await _logica.EliminarProductoEnCarritoCompra((int)carritoCompra.idComprador!, (int)carritoCompra.idProducto!);

                if (perfil.GetType() == Type.GetType("backend.Models.Comprador"))
                {

                    Comprador? comprador = perfil as Comprador;

                    var responseData = new
                    {
                        CarritoCompra = comprador.CarritoCompra,
                    };
                    return Ok(responseData);

                }
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    response = ex.Message,
                };
                return BadRequest(errorResponse);
            }

            var error = new
            {
                response = "No se trata de un comprador",
            };
            return BadRequest(error);
        }
        // ----------------------------------------------- //

    }
}
