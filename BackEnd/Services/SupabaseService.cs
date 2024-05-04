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

namespace backend.Services
{
    public class SupabaseService : Interfaz
    {
        // private static SupabaseService _instancia;
        private readonly Supabase.Client _supabaseClient;

        public SupabaseService(IConfiguration configuration)
        {
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
        

         public async Task<int> InsertarUser(UsuarioBD nuevouser)
        { 
            var result = await _supabaseClient
                            .From<UsuarioBD>()
                            .Insert(nuevouser);
            Console.WriteLine("User insertado correctamente en Supabase.");
            return result.Model!.Id;
        }

        public async Task InsertarCompradorLuis(CompradorBD comprador)
        {
            await _supabaseClient
                    .From<CompradorBD>()
                    .Insert(comprador);
            Console.WriteLine("Comprador insertado correctamente en Supabase.");
        }

        public async Task<bool> UsuarioExistePorApodo(string apodo)
        {
        // Realizar una consulta a la tabla de usuarios para verificar si existe un usuario con el apodo dado
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Nick_name == apodo)
                                .Get();

            // Si la consulta devuelve algún resultado, significa que el usuario existe
            return result.Models.Any();
        }

        public async Task<UsuarioBD> UserByNick(string filtro)
        {
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Nick_name == filtro)
                                .Get();
            
                                
            UsuarioBD users = result.Model!;
            return users;                    
        }

        public async Task<List<UsuarioBD>> GetAllUsers()
        {
            var users = await _supabaseClient
                                .From<UsuarioBD>()
                                .Get();

            List <UsuarioBD> allusers = users.Models;
            return allusers;
        
        }

        public async Task<List<ProductoBD>> GetAllProducts()
        {
            var productos = await _supabaseClient
                                .From<ProductoBD>()
                                .Get();

            List <ProductoBD> productos1 = productos.Models;
            return productos1;
        }

        public async Task<List<ArticuloBD>> GetAllArticles()
        {
            var articulos = await _supabaseClient
                                .From<ArticuloBD>()
                                .Get();

            List <ArticuloBD> articulos1 = articulos.Models;
            return articulos1;
        }

        public async Task InsertarCarrito(CarritoCompraBD nuevocarrito)
        {
            await _supabaseClient
                    .From<CarritoCompraBD>()
                    .Insert(nuevocarrito);
            Console.WriteLine("Carrito insertado correctamente en Supabase.");
        }

        public async Task<List<ArticuloBD>> ObtenerArticulosPorCategoria(string categoria)
        {
            var result = await _supabaseClient
                    .From<ArticuloBD>()
                    .Where(c => c.Categoria == categoria)
                    .Get();

            List<ArticuloBD> articulos = result.Models;
            return articulos;
        }

        public async Task<List<ProductoBD>> ObtenerProductosPorID(int id)
        {
            var result = await _supabaseClient
                                .From<ProductoBD>()
                                .Where(a => a.Id_articulo == id)
                                .Get();
            List<ProductoBD> articulos = result.Models;
            return articulos;
        }

        public async Task<UsuarioBD> ObtenerUsuarioPorID(int id){
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(u => u.Id == id)
                                .Get();
            UsuarioBD user = result.Model!;
            return user;

        }

        public async Task<ArticuloBD> ObtenerArticuloPorID(int id){
            var result = await _supabaseClient
                                .From<ArticuloBD>()
                                .Where(u => u.Id == id)
                                .Get();
            ArticuloBD articulo = result.Model!;
            return articulo;

        }

        public async Task<ImagenProductoBD> ObtenerImagenPorID(int id){
            var result = await _supabaseClient
                                .From<ImagenProductoBD>()
                                .Where(u => u.Id_articulo == id)
                                .Get();
            ImagenProductoBD imagen = result.Model!;
            return imagen;
        }

        //De momento no sirven

        public async Task InsertarProducto(ProductoBD nuevoProducto)
        {
            await _supabaseClient
                .From<ProductoBD>()
                .Insert(nuevoProducto);
            Console.WriteLine("Producto insertado correctamente en Supabase.");
        }

        public async Task<List<ProductoBD>> GetProductsById(int y)
        {
            var result = await _supabaseClient
                .From<ProductoBD>()
                .Where(x => x.Id == y)
                .Get();
            List <ProductoBD> productos = result.Models;
            return productos;
        }

        public async Task EliminarProducto(ProductoBD producto)
        {
            await _supabaseClient
                .From<ProductoBD>()
                .Delete(producto);
        }

        public async Task<ProductoBD> ProductByPrice(int filtro)
        {
            var result = await _supabaseClient
                                .From<ProductoBD>()
                                .Where(x => x.Precio_cents == filtro)
                                .Get();
            
                                
            ProductoBD users = result.Model!;
            return users;                    
        }

        public async Task<List<CarritoCompraBD>> GetChart()
        {
            var productos = await _supabaseClient
                                .From<CarritoCompraBD>()
                                .Get();

            List <CarritoCompraBD> productos1 = productos.Models;
            return productos1;
        }

        //No se usa
        public async Task<List<CompradorBD>> GetAllBuyers()
        {
            var users = await _supabaseClient
                                .From<CompradorBD>()
                                .Get();

            List <CompradorBD> allusers = users.Models;
            return allusers;
        
        }

        
        public async Task<UsuarioBD> UserByAge(int filtro)
        {
            var result = await _supabaseClient
                                .From<UsuarioBD>()
                                .Where(x => x.Edad == filtro)
                                .Get();
            
                                
            UsuarioBD users = result.Model!;
            return users;                    
        }


        public async Task<UsuarioBD> UpdateAgeUser(UsuarioBD usuario,int edad1 ,int edad)
        {
            await _supabaseClient
                    .From<UsuarioBD>()
                    .Where(x => x.Edad == edad1)
                    .Set(x => x.Edad, edad)
                    .Update();

            return usuario;
        }

    }
}
