using backend.Services;
using backend.Models;
using System.Collections.Generic;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        private readonly Interfaz interf;
        private Usuario userlogin;
        public LogicaClase(Interfaz interf)
        {
            this.interf = interf;
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
                 Usuario usuario = new Usuario{
                    Nombre = nombre,
                    Nick_name = nick_name,
                    Contraseña = contraseña,
                    Email = email,
                    Edad = edad,
                };

                AddMember(usuario);
                await interf.InsertarUser(usuario);

                int userId = ObtenerUsuarioPorNick(nick_name).Id;

                Compradorluis comprador = new Compradorluis{
                    Id = userId,
                    Limite_gasto_cents_mes = limiteGasto
                };

                await interf.InsertarCompradorLuis(comprador);

            } catch(Exception){
                
                //Con esto se consigue mostrar por consola la excepción por la que no es posible registrar a un usuario, en este caso saltan las excepciones de AddMember

                throw; 

            }
             
        }

         public void AddMember(Usuario user)
        {
            IList<Usuario> allUsers = ObtenerUsuarios();

            // Verificar si ya existe un miembro con el mismo nickname
            bool nicknamebool = allUsers.Any(u => u.Nick_name == user.Nick_name);

            // Verificar si ya existe un miembro con el mismo correo electrónico
            bool emailbool = allUsers.Any(u => u.Email == user.Email);


            if (!nicknamebool && !emailbool)
            {
                interf.InsertarUser(user);
            }
            else
            {
                if (nicknamebool)
                    throw new Exception("El member con nick " + user.Nick_name + " ya existe.");

                if (emailbool)
                    throw new Exception("El member con correo electrónico " + user.Email + " ya existe.");
            }

        }

        public async Task Login(String nick, String password)
        {
            if(nick == "" || password == "" ) 
                throw new CamposVaciosException("Existen campos vacíos");

            if (!await interf.UsuarioExistePorApodo(nick)) 
                throw new UsuarioNoExisteException("El usuario no existe");

            Usuario user =  await interf.UserByNick(nick);
            
            if (!user.Contraseña!.Equals(password))
                 throw new ContraseñaIncorrectaException("Contraseña incorrecta");

            userlogin = user;
            
            Console.WriteLine("Usuario con nick :" + user.Nick_name + " y contraseña :" + user.Contraseña + " logueado");
        }

         public IList<Usuario> ObtenerUsuarios()
        {
            var productosTask = interf.GetAllUsers();
            productosTask.Wait(); 
            List<Usuario> productos1 = productosTask.Result;
            return productos1;
        }


         public  Usuario ObtenerUsuarioPorNick(string nick)
        {
            var productosTask = interf.UserByNick(nick);
            productosTask.Wait(); 
            Usuario user1 = productosTask.Result;
            return user1;
        }


        public IList<Producto> ObtenerProductos()
        {
            var productosTask = interf.GetAllProducts(); // Obtiene la tarea para obtener todos los productos
            productosTask.Wait(); // Espera a que la tarea se complete
            //return productosTask.Result;
            List<Producto> productos1 = productosTask.Result;
            return productos1;
        }

        
        public IList<Articulo> ObtenerArticulos()
        {
            var productosTask = interf.GetAllArticles(); 
            productosTask.Wait(); 
            List<Articulo> productos1 = productosTask.Result;
            return productos1;
        }

        //Creo que podrá servir para la búsqueda de productos, para mostrar sólo los que correspondan con el string que entra por parámetros
        public IList<Articulo> GetArticlesByName(string keyWords)
        {
            IList<Articulo> allContents = ObtenerArticulos();
            allContents = allContents.Where(c => c.Nombre==keyWords).ToList();
            return allContents.ToList();
        }

        //Supongo que este método es para obtener el carrito de la compra de un usuario 
        public IList<CarritoCompra> GetChartByUser(Usuario user)
        {
            IList<CarritoCompra> allContents = ObtenerChart();
            allContents = allContents.Where(c => c.Id_usuario==user.Id).ToList();
            return allContents.ToList();
        }

        public IList<Producto> GetProductByChart(CarritoCompra carr)
        {
            IList<Producto> allContents = ObtenerProductos();
            allContents = allContents.Where(c => c.Id==carr.Id_producto).ToList();
            return allContents.ToList();
        }
         public IList<Articulo> GetArticleByProduct(Producto prod)
        {
            IList<Articulo> allContents = ObtenerArticulos();
            allContents = allContents.Where(c => c.Id==prod.Id_articulo).ToList();
            return allContents.ToList();
        } 

        public void AgregarAlCarrito(int usuarioId, int productoId)
        {
            // Aquí iría la lógica para insertar el nuevo elemento en la tabla CarritoCompra
            // Por ejemplo:
            CarritoCompra nuevoElemento = new CarritoCompra
            {
                Id_usuario = usuarioId,
                Id_producto = productoId
                // Puedes añadir otros campos si los necesitas, como cantidad, fecha, etc.
            };

            interf.InsertarCarrito(nuevoElemento);
            
        }

        




        //Supongo que este método es para obtener el carrito de la compra de un usuario
        public IList<CarritoCompra> ObtenerChart()
        {
            var productosTask = interf.GetChart();
            productosTask.Wait(); 
            List<CarritoCompra> productos1 = productosTask.Result;
            return productos1;
        }

        //NO SE USA, TENDRÁ USO EN EL FUTURO?
        public IList<Producto> GetContentsByParameters2(int keyWords)
        {
            IList<Producto> allContents = ObtenerProductos();
            allContents = allContents.Where(c => c.Precio_cents==keyWords).ToList();
            return allContents.ToList();
        }


        public Usuario UpdateEdadUsuario(Usuario usuario,int edad)
        {
            var usuario1 = interf.UpdateAgeUser(usuario,usuario.Edad,edad);
            usuario1.Wait();
            Usuario user1 = usuario1.Result;
            
            return user1;

        }

        public  Producto ObtenerProductoPorPrecio(int precio)
        {
            var productosTask = interf.ProductByPrice(precio); 
            productosTask.Wait(); 
            Producto user1 = productosTask.Result;
            return user1;
        }




    }

}