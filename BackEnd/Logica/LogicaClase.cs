using backend.Services;
using backend.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;
using backend.Conversiones;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        private readonly Interfaz interf;
        private readonly IConversiones convertir;
        private Usuario userlogin;
        public LogicaClase(Interfaz interf, IConversiones convertir)
        {
            this.interf = interf;
            this.convertir = convertir;
        }


        public Usuario UserLogged()
        {
            Usuario user = userlogin;
            return user;
        }

        //Probablemente sea borrado
        public void Logout()
        {
            if(userlogin == null) throw new Exception("Usuario no loggeado");
            userlogin = null;
            DateTime fechaacceso = DateTime.Now;
        }

        //Creo que así no peta, REVISAAAAAR!!!
         public async void AñadirComprador(string nombre, string nick_name, string contraseña, string email, int edad, int limiteGasto)
        { 
            
            try{

                Usuario usuario = new Usuario(nombre, nick_name, contraseña, email, edad);

                AddMember(usuario);
                int userId = await interf.InsertarUser(convertir.ConvertirUsuario(usuario));
                //usuario.setId(userId);

                //int userId = (ObtenerUsuarioPorNick(nick_name)).getId();
                Console.WriteLine(userId);

                if(limiteGasto > 0) {
                   
                    Comprador comprador = new Comprador(nombre, nick_name, contraseña, email, edad, limiteGasto);
                    await interf.InsertarCompradorLuis(convertir.ConvertirComprador(comprador, userId));

                 }
                

            } catch(Exception){
                
                //Con esto se consigue mostrar por consola la excepción por la que no es posible registrar a un usuario, en este caso saltan las excepciones de AddMember

                throw; 

            }
             
        }

         public void AddMember(Usuario user)
        {
            IList<Usuario> allUsers = ObtenerUsuarios();

            // Verificar si ya existe un miembro con el mismo nickname
            bool nicknamebool = allUsers.Any(u => u.getNick_name() == user.getNick_name());

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.getEmail() == user.getEmail());


            if (!nicknamebool && !emailbool)
            {
                //interf.InsertarUser(convertir.ConvertirUsuario(user));
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + user.getNick_name() + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + user.getEmail() + " ya existe.");
            }

        }

        public async Task Login(String nick, String password)
        {
            if(nick == "" || password == "" ) 
                throw new CamposVaciosException("Existen campos vacíos");

            if (!await interf.UsuarioExistePorApodo(nick)) 
                throw new UsuarioNoExisteException("El usuario no existe");

            Usuario user =  convertir.ConvertirBDaUsuario(await interf.UserByNick(nick));
            
            if (!user.getContraseña()!.Equals(password))
                 throw new ContraseñaIncorrectaException("Contraseña incorrecta");

            userlogin = user;
            
            Console.WriteLine("Usuario con nick :" + user.getNick_name() + " y contraseña :" + user.getContraseña() + " logueado");
        }

         public IList<Usuario> ObtenerUsuarios()
        {
            var productosTask = interf.GetAllUsers();
            productosTask.Wait(); 
            List<Usuario> productos1 = convertir.ConvertirListaBDaUsuario(productosTask.Result);
            return productos1;
        }


         public  Usuario ObtenerUsuarioPorNick(string nick)
        {
            var productosTask = interf.UserByNick(nick);
            productosTask.Wait(); 
            Usuario user1 = convertir.ConvertirBDaUsuario(productosTask.Result);
            return user1;
        }


        public IList<Producto> ObtenerProductos()
        {
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Producto> productos1 = convertir.ConvertirListaBDaProducto(productosTask.Result);
            return productos1;
        }

        
        public IList<Articulo> ObtenerArticulos()
        {
            var productosTask = interf.GetAllArticles(); 
            productosTask.Wait(); 
            List<Articulo> productos1 = convertir.ConvertirListaBDaArticulo(productosTask.Result);
            return productos1;
        }

        //Creo que podrá servir para la búsqueda de productos, para mostrar sólo los que correspondan con el string que entra por parámetros
        public IList<Articulo> GetArticlesByName(string keyWords)
        {
            IList<Articulo> allContents = ObtenerArticulos();
            allContents = allContents.Where(c => c.getNombre() == keyWords).ToList();
            return allContents.ToList();
        }

        //Supongo que este método es para obtener el carrito de la compra de un usuario 
        public IList<CarritoCompra> GetChartByUser(Usuario user)
        {
            IList<CarritoCompra> allContents = ObtenerChart();
            allContents = allContents.Where(c => c.getId_Usuario() == user.getId()).ToList();
            return allContents.ToList();
        }

        public IList<Producto> GetProductByChart(CarritoCompra carr)
        {
            IList<Producto> allContents = ObtenerProductos();
            allContents = allContents.Where(c => c.getId() == carr.getId_Producto()).ToList();
            return allContents.ToList();
        }
         public IList<Articulo> GetArticleByProduct(Producto prod)
        {
            IList<Articulo> allContents = ObtenerArticulos();
            allContents = allContents.Where(c => c.getId() == prod.getId_Articulo()).ToList();
            return allContents.ToList();
        } 

        public void AgregarAlCarrito(int usuarioId, int productoId)
        {
            CarritoCompra nuevoElemento = new CarritoCompra(usuarioId, productoId);
            interf.InsertarCarrito(convertir.ConvertirCarritoCompra(nuevoElemento));
        }

        public IList<Producto> ObtenerProductosPorCategoria(string categoria)
        {
            var articulos = interf.ObtenerArticulosPorCategoria(categoria);
            articulos.Wait();
            IList<Producto> listaProductos = FiltrarArticulos(convertir.ConvertirListaBDaArticulo(articulos.Result));
            return listaProductos;
        }

        public  IList<Producto>  FiltrarArticulos(IList<Articulo> filtrados) 
        {
            
            IList<Producto> listaProductos = new List<Producto>();

            foreach(var articuloFiltrado in filtrados)
            {
                var ejem =  interf.ObtenerProductosPorID(articuloFiltrado.getId());
                ejem.Wait();
                List<Producto> e1 = convertir.ConvertirListaBDaProducto(ejem.Result.OrderBy(producto => producto.Precio_cents).ToList());
                //hacer algo q compruebe q aún no está
                listaProductos.Add(e1.First());
            }
            return listaProductos;
        }


        //Supongo que este método es para obtener el carrito de la compra de un usuario
        public IList<CarritoCompra> ObtenerChart()
        {
            var productosTask = interf.GetChart();
            productosTask.Wait(); 
            List<CarritoCompra> productos1 = convertir.ConvertirListaBDaCarritoCompra(productosTask.Result);
            return productos1;
        }


        public Usuario UpdateEdadUsuario(Usuario usuario,int edad)
        {
            var usuario1 = interf.UpdateAgeUser(convertir.ConvertirUsuario(usuario),usuario.getEdad(),edad);
            usuario1.Wait();
            Usuario user1 = convertir.ConvertirBDaUsuario(usuario1.Result);
            
            return user1;

        }

        public  Producto ObtenerProductoPorPrecio(int precio)
        {
            var productosTask = interf.ProductByPrice(precio); 
            productosTask.Wait(); 
            Producto user1 = convertir.ConvertirBDaProducto(productosTask.Result);
            return user1;
        }

    }

}