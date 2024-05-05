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
using static System.Runtime.InteropServices.JavaScript.JSType;
using Postgrest.Models;

using backend.Models;
using backend.ModelsSupabase;
using backend.Services;

namespace backend.Conversiones{

    public class Conversiones : IConversiones{

        private readonly ISupabaseService? supabaseService;





        public UsuarioBD CompradorAUsuarioBD(Comprador comprador){

            return new UsuarioBD
            {
                Id = comprador.Id,
                Nombre = comprador.Nombre,
                Nick_name = comprador.Nick_name,
                Contraseña = comprador.Contraseña,
                Email = comprador.Email,
                Edad = comprador.Edad,
            };

        }

        public CompradorBD CompradorACompradorBD(Comprador comprador){

            return new CompradorBD
            {
                Id = comprador.Id,
                Limite_gasto_cents_mes = comprador.Limite_gasto_cents_mes
            };

        }

        public Usuario UsuarioBDAUsuario(UsuarioBD usuarioBD){

            var usuario = new Usuario(usuarioBD.Nombre, usuarioBD.Nick_name, usuarioBD.Contraseña, usuarioBD.Email);
            usuario.Edad = usuarioBD.Edad;
            return usuario;

        }

        public Comprador UsuarioBDYCompradorBDAComprador(UsuarioBD usuarioBD, CompradorBD compradorBD){
            var comprador = new Comprador(usuarioBD.Nombre, usuarioBD.Nick_name, usuarioBD.Contraseña, usuarioBD.Email);
            comprador.Id = usuarioBD.Id;
            comprador.Edad = usuarioBD.Edad;
            comprador.Limite_gasto_cents_mes = compradorBD.Limite_gasto_cents_mes;
            comprador.ImagenesUrl = usuarioBD.ImagenUrl;
            return comprador;
        }


        public Vendedor UsuarioBDAVendedor(UsuarioBD usuarioBD)
        {
            var vendedor = new Vendedor(usuarioBD.Nombre, usuarioBD.Nick_name, usuarioBD.Contraseña, usuarioBD.Email);
            vendedor.Id = usuarioBD.Id;
            vendedor.Edad = usuarioBD.Edad;
            vendedor.ImagenesUrl = usuarioBD.ImagenUrl;
            return vendedor;
        }

        public Productor UsuarioBDAProductor(UsuarioBD usuarioBD)
        {
            var productor = new Productor(usuarioBD.Nombre, usuarioBD.Nick_name, usuarioBD.Contraseña, usuarioBD.Email);
            productor.Id = usuarioBD.Id;
            productor.Edad = usuarioBD.Edad;
            productor.ImagenesUrl = usuarioBD.ImagenUrl;
            return productor;
        }

        public async Task<Articulo>  ArticuloBDAArticulo(ArticuloBD articuloBD){
            Productor? productor = await  supabaseService.ObtenerUsuarioPorId(articuloBD.Id_productor) as Productor; 
            return new Articulo(articuloBD.Nombre, articuloBD.Categoria, articuloBD.Edad_min,
            articuloBD.Consejos_utilizacion, articuloBD.Consejos_retirada, articuloBD.Origen, articuloBD.Proceso_produccion,
            articuloBD.Impacto_ambiental_social, articuloBD.Contribucion_ods, productor!);
        }

        public  Producto ProductoBDAProducto(ProductoBD productoBD ){

            var art =  supabaseService.ObtenerArticuloPorId(productoBD.Id_articulo);
            art.Wait();
            Articulo articulo = art.Result as Articulo;

            var vend = supabaseService.ObtenerUsuarioPorId(productoBD.Id_vendedor);
            vend.Wait();
            Vendedor vendedor = (vend.Result as Vendedor)!;

            return new Producto(productoBD.Precio_cents, productoBD.Unidades,vendedor!,articulo);
        }
    }




        

    //     public Producto ConvertirBDaProducto(ProductoBD productoBD){

    //         Producto productoConvertido = new Producto(productoBD.Id, productoBD.Precio_cents, productoBD.Unidades, productoBD.Id_vendedor, productoBD.Id_articulo, productoBD.Id_vendedor, ConvertirArticulo(productoBD.Articulo!));
    //         return productoConvertido;

    //     }



    //     public List<Producto> ConvertirListaBDaProducto(List<ProductoBD> listaProductos){
            
    //         List<Producto> listaProductosConvertida = new List<Producto>();
    //         foreach(var product in listaProductos)
    //         {
    //             listaProductosConvertida.Add(ConvertirBDaProducto(product));
    //         }
    //         return listaProductosConvertida;
    //     }

    //     public List<Usuario> ConvertirListaBDaUsuario(List<UsuarioBD> listaUsuario){
            
    //         List<Usuario> listaUsuarioConvertida = new List<Usuario>();
    //         foreach(var user in listaUsuario)
    //         {
    //             listaUsuarioConvertida.Add(ConvertirBDaUsuario(user));
    //         }
    //         return listaUsuarioConvertida;
    //     }

    //     public List<Articulo> ConvertirListaBDaArticulo(List<ArticuloBD> listaArticulos){
            
    //         List<Articulo> listaArticulosConvertida = new List<Articulo>();
    //         foreach(var product in listaArticulos)
    //         {
    //             listaArticulosConvertida.Add(ConvertirArticulo(product));
    //         }
    //         return listaArticulosConvertida;
    //     }

    //     public List<CarritoCompra> ConvertirListaBDaCarritoCompra(List<CarritoCompraBD> listaCarrito){
            
    //         List<CarritoCompra> listaCarritoConvertida = new List<CarritoCompra>();
    //         foreach(var carrito in listaCarrito)
    //         {
    //             listaCarritoConvertida.Add(ConvertirBDaCarritoCompra(carrito));
    //         }
    //         return listaCarritoConvertida;
    //     }
        

    //     public Vendedor ConvertirVendedor(VendedorBD vendedor){

    //         Vendedor vendedorConvertido = new Vendedor(vendedor.Nombre!, vendedor.Nick_name!, vendedor.Contraseña!, vendedor.Email!, vendedor.Edad);
    //         return vendedorConvertido;
    //     }

    //     public Articulo ConvertirArticulo(ArticuloBD articulo){

    //         Articulo articuloConvertido = new Articulo(articulo.Id, articulo.Nombre!, articulo.Categoria!, articulo.Edad_min, articulo.Consejos_utilizacion!, articulo.Consejos_retirada!, articulo.Origen!, articulo.Proceso_produccion!, articulo.Impacto_ambiental_social!, articulo.Contribucion_ods!, articulo.Id_producto);
    //         return articuloConvertido;
    //     }

    //     public CarritoCompraBD ConvertirCarritoCompra(CarritoCompra carrito){
    //         CarritoCompraBD carritoConvertido = new CarritoCompraBD
    //         {
    //             Id_usuario = carrito.getId_Usuario(),
    //             Id_producto = carrito.getId_Producto()   
    //         };
    //         return carritoConvertido;
    //     }

    //     public CarritoCompra ConvertirBDaCarritoCompra(CarritoCompraBD carrito){

    //         CarritoCompra carritoConvertido = new CarritoCompra(carrito.Id_usuario, carrito.Id_producto);
    //         return carritoConvertido;

    //     }

    // }

}