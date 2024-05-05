using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Logica;


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

        [HttpPost("Registrarse")]
        public IActionResult RegistrarComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limite_gasto_cents_mes)
        {

            //TODO: CREATE A NEW CLASS OR INTERFACE

            try{
                var comprador = new Comprador(nombre, nick_name, contraseña, email);
                comprador.Edad = edad;
                comprador.Limite_gasto_cents_mes = limite_gasto_cents_mes;
                _logica.RegistrarComprador(comprador);

            }catch(Exception ex){
                return BadRequest("fallo "+ ex.Message);
            }

            
            return Ok("TODO CORRECTOOOÇ");
            
        }


        [HttpPost("login2")]
        public async Task<IActionResult> IniciarSesion(string nick_name, string contraseña)
        {
            try
            {

            var perfil = await  _logica.IniciarSesion(nick_name, contraseña);


                if (perfil.GetType() == Type.GetType("Comprador"))
                {

                    Comprador? comprador = perfil as Comprador;

                    var responseData = new
                    {
                        Comprador = comprador,
                    };
                      return Ok(responseData);

                }}

                
                catch(UsuarioNoExisteException ex)
                {
                    return NotFound("Usuario no encontrado: " + ex.Message);
                }
                catch(ContraseñaIncorrectaException ex)
                {
                    return Unauthorized("Contraseña incorrecta: " + ex.Message);
                }
                catch(Exception ex)
                {
                    return StatusCode(500, "Error: " + ex.Message);
                }
                return StatusCode(400,"error desconocido");
            }





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
    
        

    //     //Cuerpo de las respuestas

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
