using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Supabase;
using Supabase.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using backend.ModelsSupabase;
using Postgrest.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using backend.Models;
using backend.Conversiones;
using System.Linq.Expressions;
using static Postgrest.Constants;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.VisualBasic;

namespace backend.Services
{
    public class SupabaseService : ISupabaseService
    {
        // private static SupabaseService _instancia;
        private readonly Supabase.Client _supabaseClient;
        private readonly IConversiones convertir;

        public SupabaseService(IConfiguration configuration, IConversiones convertir)
        {

            this.convertir = convertir;
            var supabaseUrl = configuration["Supabase:Url"];
            var supabaseKey = configuration["Supabase:ApiKey"];

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }

        // public static SupabaseService GetInstance(IConfiguration configuration){
        //     if(_instancia == null)

        //         _instancia = new SupabaseService(configuration);

        //     return _instancia;
        // }

        public async Task InitializeSupabaseAsync()
        {
            Console.WriteLine("Iniciando la conexión a Supabase...");
            await _supabaseClient.InitializeAsync();
            Console.WriteLine("Conexión a Supabase completada.");
        }








        public async Task AñadirComprador(CompradorBD compradorBD)
        {

            await _supabaseClient
                    .From<CompradorBD>()
                    .Insert(compradorBD);
            Console.WriteLine("Comprador insertado correctamente en Supabase.");


        }


        public async Task<int> AñadirUsuario(UsuarioBD nuevouser)
        {
            var result = await _supabaseClient
                            .From<UsuarioBD>()
                            .Insert(nuevouser);
            Console.WriteLine("User insertado correctamente en Supabase.");
            return (int)result.Model!.Id!;
        }



        public async Task<bool> ExisteNickNameEnUsuario(string nickName)
        {
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Nick_name == nickName)
                                .Get();

            return result.Models.Any();
        }


        public async Task<bool> ExisteEmailEnUsuario(string email)
        {

            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Email == email)
                                .Get();

            return result.Models.Any();
        }


        public async Task<System.Object> ObtenerUsuarioPorNickName(string nickName)
        {
            var usuario = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Nick_name == nickName)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            return await ObtenerUsuarioPorUsurioBD(usuarioBD);

        }

        public async Task<System.Object> ObtenerUsuarioPorId(int id_usuario)
        {
            var usuario = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Id == id_usuario)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El id '" + id_usuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            return await ObtenerUsuarioPorUsurioBD(usuarioBD);

        }


        private async Task<System.Object> ObtenerUsuarioPorUsurioBD(UsuarioBD usuarioBD)
        {

            try
            {



                var primero = _supabaseClient
                                    .From<CompradorBD>();
                var segundo = primero.Where(x => x.Id == usuarioBD.Id!);

                var tercero = await segundo.Get();


                var comprador = await _supabaseClient
                                    .From<CompradorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

                if (comprador.Models.Any()) return convertir.UsuarioBDYCompradorBDAComprador(usuarioBD, comprador.Model!);

                var vendedor = await _supabaseClient
                                    .From<VendedorBD>()
                                    .Where(x => x.Id == usuarioBD.Id)
                                    .Get();

                if (vendedor.Models.Any()) return convertir.UsuarioBDAVendedor(usuarioBD);

                var productor = await _supabaseClient
                                    .From<ProductorBD>()
                                    .Where(x => x.Id == usuarioBD.Id)
                                    .Get();

                if (productor.Models.Any()) return convertir.UsuarioBDAProductor(usuarioBD);

                throw new Exception("El usuario no corresponde con ningun comprador, vendedor o productor.");

            }

            //TODO: DELETE?
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception();
            }




        }


        public async Task<Articulo> ObtenerArticuloPorId(int articuloId)
        {

            var articulo = await _supabaseClient
                            .From<ArticuloBD>()
                            .Where(x => x.Id == articuloId)
                            .Get();

            return ConvertirArticuloBDAArticulo(articulo.Model!);

        }


        public async Task<ICollection<Producto>> ObtenerCarritoCompra(int compradorId)
        {

            var carrito = await _supabaseClient
                            .From<CarritoCompraBD>()
                            .Where(x => x.Id_comprador == compradorId)
                            .Get();

            List<int> lista = new List<int>();
            lista = carrito.Models.ConvertAll((CarritoCompraBD carrito) => carrito.Id_producto);


            List<ProductoBD> productosBD = new List<ProductoBD>();
            //TODO: make a for
            foreach (var idProducto in lista)
            {

                var productos = await _supabaseClient
                        .From<ProductoBD>()
                        .Where(x => x.Id == idProducto)
                        .Get();

                if (productos.Model != null) { productosBD.Add(productos.Model); }


            }




            Console.WriteLine("");

            return productosBD.ConvertAll<Producto>(ConvertirProductoBDAProducto);
        }



        public async void AñadirProductoACarritoCompra(int compradorId, int productoId)
        {

            try
            {

                //todo: add to convertir
                CarritoCompraBD carritoCompraBD = new CarritoCompraBD
                {
                    Id_comprador = compradorId,
                    Id_producto = productoId
                };


                await _supabaseClient.From<CarritoCompraBD>().Insert(carritoCompraBD);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            };




        }


        public async void EliminarProductoEnCarritoCompra(int compradorId, int productoId)
        {
            //todo: add to convertir
            CarritoCompraBD carritoCompraBD = new CarritoCompraBD
            {
                Id_comprador = compradorId,
                Id_producto = productoId,
            };


            var result = await _supabaseClient
            .From<CarritoCompraBD>().Delete(carritoCompraBD);



        }


        public async Task<List<Articulo>> ObtenerArticulosPorCategoria(string categoria)
        {
            var result = await _supabaseClient
                    .From<ArticuloBD>()
                    .Where(c => c.Categoria == categoria)
                    .Get();

            return result.Models.ConvertAll<Articulo>(ConvertirArticuloBDAArticulo);
        }


        private async Task<List<string>> ObtenerImagenesArticuloPorId(int idArticulo)
        {
            var result = await _supabaseClient
                    .From<ImagenArticuloBD>()
                    .Where(x => x.idArticulo == idArticulo)
                    .Get();

            return result.Models.ConvertAll<string>(convertir.ImagenArticuloBDAImagen);
        }




        private Articulo ConvertirArticuloBDAArticulo(ArticuloBD articuloBD)
        {
            var usu = ObtenerUsuarioPorId(articuloBD.Id_productor);
            usu.Wait();
            Productor? productor = usu.Result as Productor;

            var imagenes = ObtenerImagenesArticuloPorId(articuloBD.Id);
            imagenes.Wait();
            List<string> imagenesArticulo = imagenes.Result as List<string>;

            return convertir.ArticuloBDAArticulo(articuloBD, productor!, imagenesArticulo);


        }

        public async Task<List<Producto>> ObtenerProductosPorIDArticulo(int idArticulo, Articulo? articulo)
        {
            var result = await _supabaseClient
                                .From<ProductoBD>()
                                .Where(a => a.Id_articulo == idArticulo)
                                .Get();



            return result.Models.ConvertAll(ConvertirProductoBDAProducto);
        }

        public Producto ConvertirProductoBDAProducto(ProductoBD productoBD)
        {

            var vend = ObtenerUsuarioPorId(productoBD.Id_vendedor);
            vend.Wait();
            Vendedor vendedor = (vend.Result as Vendedor)!;

            var art = ObtenerArticuloPorId(productoBD.Id_articulo);
            art.Wait();
            Articulo articulo = art.Result as Articulo;



            return convertir.ProductoBDAProducto(productoBD, vendedor!, articulo!);
        }
















        // public async Task<List<UsuarioBD>> ObtenerTodosLosUsuarios()
        // {
        //     var users = await _supabaseClient
        //                         .From<UsuarioBD>()
        //                         .Get();

        //     return users.Models;

        // }






        //###############################################





        // public async Task<List<ProductoBD>> GetAllProducts()
        // {
        //     var productos = await _supabaseClient
        //                         .From<ProductoBD>()
        //                         .Get();

        //     List <ProductoBD> productos1 = productos.Models;
        //     return productos1;
        // }

        // public async Task<List<ArticuloBD>> GetAllArticles()
        // {
        //     var articulos = await _supabaseClient
        //                         .From<ArticuloBD>()
        //                         .Get();

        //     List <ArticuloBD> articulos1 = articulos.Models;
        //     return articulos1;
        // }

        // public async Task InsertarCarrito(CarritoCompraBD nuevocarrito)
        // {
        //     await _supabaseClient
        //             .From<CarritoCompraBD>()
        //             .Insert(nuevocarrito);
        //     Console.WriteLine("Carrito insertado correctamente en Supabase.");
        // }

        // public async Task<List<ArticuloBD>> ObtenerArticulosPorCategoria(string categoria)
        // {
        //     var result = await _supabaseClient
        //             .From<ArticuloBD>()
        //             .Where(c => c.Categoria == categoria)
        //             .Get();

        //     List<ArticuloBD> articulos = result.Models;
        //     return articulos;
        // }

        // public async Task<List<ProductoBD>> ObtenerProductosPorID(int id)
        // {
        //     var result = await _supabaseClient
        //                         .From<ProductoBD>()
        //                         .Where(a => a.Id_articulo == id)
        //                         .Get();
        //     List<ProductoBD> articulos = result.Models;
        //     return articulos;
        // }

        // //De momento no sirven

        // public async Task InsertarProducto(ProductoBD nuevoProducto)
        // {
        //     await _supabaseClient
        //         .From<ProductoBD>()
        //         .Insert(nuevoProducto);
        //     Console.WriteLine("Producto insertado correctamente en Supabase.");
        // }

        // public async Task<List<ProductoBD>> GetProductsById(int y)
        // {
        //     var result = await _supabaseClient
        //         .From<ProductoBD>()
        //         .Where(x => x.Id == y)
        //         .Get();
        //     List <ProductoBD> productos = result.Models;
        //     return productos;
        // }

        // public async Task EliminarProducto(ProductoBD producto)
        // {
        //     await _supabaseClient
        //         .From<ProductoBD>()
        //         .Delete(producto);
        // }

        // public async Task<ProductoBD> ProductByPrice(int filtro)
        // {
        //     var result = await _supabaseClient
        //                         .From<ProductoBD>()
        //                         .Where(x => x.Precio_cents == filtro)
        //                         .Get();


        //     ProductoBD users = result.Model!;
        //     return users;                    
        // }

        // public async Task<List<CarritoCompraBD>> GetChart()
        // {
        //     var productos = await _supabaseClient
        //                         .From<CarritoCompraBD>()
        //                         .Get();

        //     List <CarritoCompraBD> productos1 = productos.Models;
        //     return productos1;
        // }

        // //No se usa
        // public async Task<List<CompradorBD>> GetAllBuyers()
        // {
        //     var users = await _supabaseClient
        //                         .From<CompradorBD>()
        //                         .Get();

        //     List <CompradorBD> allusers = users.Models;
        //     return allusers;

        // }


        // public async Task<UsuarioBD> UserByAge(int filtro)
        // {
        //     var result = await _supabaseClient
        //                         .From<UsuarioBD>()
        //                         .Where(x => x.Edad == filtro)
        //                         .Get();


        //     UsuarioBD users = result.Model!;
        //     return users;                    
        // }


        // public async Task<UsuarioBD> UpdateAgeUser(UsuarioBD usuario,int edad1 ,int edad)
        // {
        //     await _supabaseClient
        //             .From<UsuarioBD>()
        //             .Where(x => x.Edad == edad1)
        //             .Set(x => x.Edad, edad)
        //             .Update();

        //     return usuario;
        // }

    }
}
