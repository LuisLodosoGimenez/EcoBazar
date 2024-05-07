using backend.Services;
using backend.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;
using backend.Conversiones;
using System.Collections.ObjectModel;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        private readonly ISupabaseService supabaseService;
        private readonly IConversiones convertir;
        public LogicaClase(ISupabaseService supabaseService, IConversiones convertir)
        {
            this.supabaseService = supabaseService;
            this.convertir = convertir;
        }


        public async Task RegistrarComprador(Comprador comprador)
        {
            if (supabaseService.ExisteNickNameEnUsuario(comprador.Nick_name).Result)
            {
                throw new Exception("NickName '" + comprador.Nick_name + "' ya en uso.");

            }
            else if (supabaseService.ExisteEmailEnUsuario(comprador.Email).Result)
            {
                throw new Exception("Email '" + comprador.Email + "' ya en uso.");
            }
            else
            {
                var idUsuario = await supabaseService.AñadirUsuario(convertir.CompradorAUsuarioBD(comprador));
                comprador.Id = idUsuario;
                await supabaseService.AñadirComprador(convertir.CompradorACompradorBD(comprador));

            }
        }




        public async Task<object> IniciarSesion(string nickName, string contraseña)
        {


            var objeto = await supabaseService.ObtenerUsuarioPorNickName(nickName);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;



                if (comprador!.Contraseña != contraseña) throw new Exception("Contraseña incorrecta");
                ICollection<Producto> productos = await supabaseService.ObtenerCarritoCompra((int)comprador.Id!);

                comprador.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("ERROR DESCONOCIDOOO");
        }



        public async Task<IList<Producto>> ObtenerProductosPorCategoria(string categoria)
        {
            List<Articulo> articulos = await supabaseService.ObtenerArticulosPorCategoria(categoria);


            //TODO: en vez de obtener todos los productos de un artículo para que se complete 
            //      su lista de productos, obtener directamente el más barato.
            //      el diseño de las clases de la logica se queda así por si en un futuro es de utilidad
            //      darle el uso correspondiente a según que atributo.

            articulos = articulos.ConvertAll(ObtenerProductosDeArticulo);



            List<Producto> productosMasBaratos = new List<Producto>();

            foreach (var articulo in articulos)
            {


                Producto? producto = ObtenerProductoMasBaratoDeArticulo(articulo);
                if (producto != null) productosMasBaratos.Add(producto);
            }

            return productosMasBaratos;



        }


        private Articulo ObtenerProductosDeArticulo(Articulo articulo)
        {

            var result = supabaseService.ObtenerProductosPorIDArticulo((int)articulo.Id!, articulo);
            result.Wait();

            List<Producto> productos = result.Result;

            articulo.Productos = productos;
            return articulo;

        }



        private Producto? ObtenerProductoMasBaratoDeArticulo(Articulo articulo)
        {
            List<Producto> productosOrdenados = articulo.Productos.OrderBy(producto => producto.Precio_cents).ToList();

            if (productosOrdenados.Count != 0) return productosOrdenados.First();
            else return null;
        }


        public async Task<Comprador> AñadirProductoACarritoCompra(int idComprador, int idProducto)
        {


            var objeto = await supabaseService.ObtenerUsuarioPorId(idComprador);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;
                await supabaseService.AñadirProductoACarritoCompra((int)comprador!.Id!, idProducto);
                ICollection<Producto> productos = await supabaseService.ObtenerCarritoCompra(idComprador);

                comprador!.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("EL ID DE USUARIO NO ES DE COMPRADOR");
        }

        public async Task<Comprador> EliminarProductoEnCarritoCompra(int idComprador, int idProducto)
        {


            var objeto = await supabaseService.ObtenerUsuarioPorId(idComprador);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;
                await supabaseService.EliminarProductoEnCarritoCompra((int)comprador!.Id!, idProducto);
                ICollection<Producto> productos = await supabaseService.ObtenerCarritoCompra(idComprador);

                comprador!.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("EL ID DE USUARIO NO ES DE COMPRADOR");
        }








        // public IList<Producto> ObtenerProductos()
        // {
        //     var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
        //     productosTask.Wait(); // Espera a que la tarea se complete
        //     //return productosTask.Result;
        //     List<Producto> productos1 = convertir.ConvertirListaBDaProducto(productosTask.Result);
        //     return productos1;
        // }


        // public IList<Articulo> ObtenerArticulos()
        // {
        //     var productosTask = interf.GetAllArticles(); 
        //     productosTask.Wait(); 
        //     List<Articulo> productos1 = convertir.ConvertirListaBDaArticulo(productosTask.Result);
        //     return productos1;
        // }

        // //Creo que podrá servir para la búsqueda de productos, para mostrar sólo los que correspondan con el string que entra por parámetros
        // public IList<Articulo> GetArticlesByName(string keyWords)
        // {
        //     IList<Articulo> allContents = ObtenerArticulos();
        //     allContents = allContents.Where(c => c.Nombre == keyWords).ToList();
        //     return allContents.ToList();
        // }

        // /*
        // public IList<CarritoCompra> GetChartByUser(Usuario user)
        // {
        //     IList<CarritoCompra> allContents = ObtenerChart();
        //     allContents = allContents.Where(c => c.getId_Usuario() == user.getId()).ToList();
        //     return allContents.ToList();
        // }
        // */


        // //Supongo que este método es para obtener el carrito de la compra de un usuario 




        // // public IList<Producto> GetProductByChart(CarritoCompra carr)
        // // {
        // //     IList<Producto> allContents = ObtenerProductos();
        // //     allContents = allContents.Where(c => c.getId() == carr.getId_Producto()).ToList();
        // //     return allContents.ToList();
        // // }




        // //TODO: QUE UTILIDAD??

        // //  public IList<Articulo> GetArticleByProduct(Producto prod)
        // // {
        // //     IList<Articulo> allContents = ObtenerArticulos();
        // //     allContents = allContents.Where(c => c.Id == prod.Id_articulo).ToList();
        // //     return allContents.ToList();
        // // } 



        // //NO HAY CARRITO

        // // public void AgregarAlCarrito(int usuarioId, int productoId)
        // // {
        // //     CarritoCompra nuevoElemento = new CarritoCompra(usuarioId, productoId);
        // //     interf.InsertarCarrito(convertir.ConvertirCarritoCompra(nuevoElemento));
        // // }

        // public IList<Producto> ObtenerProductosPorCategoria(string categoria)
        // {
        //     var articulos = interf.ObtenerArticulosPorCategoria(categoria);
        //     articulos.Wait();
        //     IList<Producto> listaProductos = FiltrarArticulos(convertir.ConvertirListaBDaArticulo(articulos.Result));
        //     return listaProductos;
        // }

        // public IList<Producto> FiltrarArticulos(IList<Articulo> filtrados) 
        // {

        //     IList<Producto> listaProductos = new List<Producto>();

        //     foreach(var articuloFiltrado in filtrados)
        //     {
        //         var ejem =  interf.ObtenerProductosPorID(articuloFiltrado.getId());
        //         ejem.Wait();
        //         List<ProductoBD> e1 = ejem.Result.OrderBy(producto => producto.Precio_cents).ToList();
        //         //hacer algo q compruebe q aún no está
        //         listaProductos.Add(convertir.ConvertirBDaProducto(e1.First()));
        //     }
        //     return listaProductos;
        // }


        // //Supongo que este método es para obtener el carrito de la compra de un usuario
        // public IList<CarritoCompra> ObtenerChart()
        // {
        //     var productosTask = interf.GetChart();
        //     productosTask.Wait(); 
        //     List<CarritoCompra> productos1 = convertir.ConvertirListaBDaCarritoCompra(productosTask.Result);
        //     return productos1;
        // }


        // public Usuario UpdateEdadUsuario(Usuario usuario,int edad)
        // {
        //     var usuario1 = interf.UpdateAgeUser(convertir.ConvertirUsuario(usuario),usuario.getEdad(),edad);
        //     usuario1.Wait();
        //     Usuario user1 = convertir.ConvertirBDaUsuario(usuario1.Result);

        //     return user1;

        // }

        // public  Producto ObtenerProductoPorPrecio(int precio)
        // {
        //     var productosTask = interf.ProductByPrice(precio); 
        //     productosTask.Wait(); 
        //     Producto user1 = convertir.ConvertirBDaProducto(productosTask.Result);
        //     return user1;
        // }

    }

}