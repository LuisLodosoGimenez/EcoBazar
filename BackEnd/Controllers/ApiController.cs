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
            public string? TipoRegistro { get; set; }
            public string? Nombre { get; set; }
            public string? NickName { get; set; }
            public string? Contraseña { get; set; }
            public string? Email { get; set; }
            public int? Edad { get; set; }
            public int? LimiteGasto { get; set; }
        }
        [HttpPost("Registrarse")]
        public async Task<IActionResult> RegistrarComprador(Registro registro)
        {

            try
            {

                await _logica.RegistrarComprador(registro);

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
            public string? TipoSesion { get; set; }
            public string? NickName { get; set; }
            public string? Contraseña { get; set; }


        }
        [HttpPost("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(InicioSesion inicioSesion)
        {
            try
            {
                var perfil = await _logica.IniciarSesion(inicioSesion.NickName!, inicioSesion.Contraseña!, inicioSesion.TipoSesion!);

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
                Comprador comprador = await _logica.AñadirProductoACarritoCompra((int)carritoCompra.idComprador!, (int)carritoCompra.idProducto!);

                var responseData = new
                {
                    CarritoCompra = comprador.CarritoCompra,
                };
                return Ok(responseData);


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


        // ------- //


        [HttpPost("EliminarProductoEnCarritoCompra")]
        public async Task<IActionResult> EliminarProductoEnCarritoCompra(CarritoCompra carritoCompra)
        {
            try
            {
                Comprador comprador = await _logica.EliminarProductoEnCarritoCompra((int)carritoCompra.idComprador!, (int)carritoCompra.idProducto!);

                var responseData = new
                {
                    CarritoCompra = comprador.CarritoCompra,
                };
                return Ok(responseData);
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
        public class CreacionPedidoPeticion
        {
            public int IdComprador { get; set; }
            public int[]? CarritoCompra { get; set; }


        }
        [HttpPost("CrearPedido")]
        public async Task<IActionResult> CrearPedidoAComprador(CreacionPedidoPeticion creacionPedidoPeticion)
        {

            try
            {
                var comp = await _logica.CrearPedidoAComprador(creacionPedidoPeticion);

                var responseData = new
                {
                    Comprador = comp,
                };
                return Ok(responseData);

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














        //     [HttpPost("login")]
        //     public IActionResult Login([FromBody] LoginRequest request)
        //     {
        //         _logica.Login(request.Nick!, request.Password!);
        //         return Ok();
        //     }

        //     [HttpPost("login2")]
        //     public async Task<IActionResult> Login2([FromBody] LoginRequest request)
        //     {
        //         try
        //         {
        //             await _logica.Login(request.Nick!, request.Password!);
        //             var perfil = _logica.ObtenerUsuarioPorNick(request.Nick!); //Obtiene al usuario
        //             var user = _logica.GetChartByUser(perfil); //Accede al carrito del usuario
        //             var productos = new List<Producto>(); //Crea una lista para almacenar los productos del usuario
        //             var items = new List<Articulo>(); //Crea una lista para almacenar los productos del usuario

        //             // Para cada carrito en la lista de carritos
        //             foreach(var product in user)
        //             {
        //                 // Obtener los productos asociados al carrito del usuario
        //                 var productItems = _logica.GetProductByChart(product);

        //                 // Agregar los productos a la lista de productos
        //                 productos.AddRange(productItems);
        //             }
        //             foreach(var prod in productos)
        //             {
        //                 // Obtener los artículos asociados al producto
        //                 var productItems = _logica.GetArticleByProduct(prod);

        //                 // Agregar los artículos a la lista de items
        //                 items.AddRange(productItems);
        //             }

        //             var responseData = new 
        //             {
        //                 Perfil = perfil,
        //                 ArticulosEnCarrito = items
        //             };

        //             return Ok(responseData);
        //         }
        //         catch(UsuarioNoExisteException ex)
        //         {
        //             return NotFound("Usuario no encontrado: " + ex.Message);
        //         }
        //         catch(ContraseñaIncorrectaException ex)
        //         {
        //             return Unauthorized("Contraseña incorrecta: " + ex.Message);
        //         }
        //         catch(Exception ex)
        //         {
        //             return StatusCode(500, "Error: " + ex.Message);
        //         }
        //     }

        //     [HttpGet("perfil/{nick}")]
        //     public IActionResult ObtenerPerfilComprador(string nick)
        //     {
        //         var perfil = _logica.ObtenerUsuarioPorNick(nick);
        //         return Ok(perfil);
        //     }

        //     [HttpGet("categoria")]
        //     public IActionResult ObtenerProductos(string categoria)
        //     {
        //         var listaProductos = _logica.ObtenerProductosPorCategoria(categoria);
        //         return Ok(listaProductos);
        //     }

        //     //Hasta el momento no sirven para nada

        //     [HttpPost("carrito")]
        //     public IActionResult AgregarAlCarrito([FromBody] CarritoCompraRequest request)
        //     {
        //         // Obtener el usuario logueado
        //         var user = _logica.UserLogged();

        //         // Verificar si el usuario está autenticado
        //         if (user == null)
        //         {
        //             // El usuario no está autenticado, puedes devolver un error o redirigir a la página de inicio de sesión
        //             return Unauthorized();
        //         }
        //         var userId = user.getId();
        //         _logica.AgregarAlCarrito(userId, request.ProductId);
        //         return Ok();
        //     }



        //     [HttpPost("carritomanual")]
        //     public IActionResult AgregarAlCarritoManual([FromBody] CarritoCompraRequest2 request)
        //     {
        //         _logica.AgregarAlCarrito(request.UserId, request.ProductId);
        //         return Ok();
        //     }

        //     [HttpGet("userlogged")]
        //     public IActionResult ObtenerUsuarioLogueado()
        //     {
        //         // Obtener el usuario logueado
        //         var user = _logica.UserLogged();

        //         // Verificar si el usuario está autenticado
        //         if (user == null)
        //         {
        //             // El usuario no está autenticado, puedes devolver un error o redirigir a la página de inicio de sesión
        //             return Unauthorized();
        //         }

        //         // Devolver el usuario logueado
        //         return Ok(user);
        //     }

        //     [HttpGet("productos")]
        //     public IActionResult GetProductos()
        //     {
        //         try
        //         {
        //         var productos = _logica.ObtenerProductos();
        //         return Ok(productos);
        //         }
        //         catch (Exception ex)
        //         {
        //             return StatusCode(500, "Internal Server Error : " + ex.Message);
        //         }
        //     }



        //     //Cuerpo de las respuestas  -->> peticiones en todo caso

        //     public class RegistroLuisRequest
        //     {
        //         public string? Nombre { get; set; }
        //         public string? Nick { get; set; }
        //         public string? Password { get; set; }
        //         public string? Email { get; set; }
        //         public int Edad { get; set; }
        //         public int LimiteGasto {get; set; }
        //     }

        //     public class CarritoCompraRequest
        //     {
        //         public int ProductId { get; set; }
        //     }

        //     public class CarritoCompraRequest2
        //     {
        //         public int UserId {get; set; }
        //         public int ProductId { get; set; }
        //     }

        //     public class LoginRequest
        //     {
        //         public string? Nick { get; set; }
        //         public string? Password { get; set; }
        //     }




    }
}
