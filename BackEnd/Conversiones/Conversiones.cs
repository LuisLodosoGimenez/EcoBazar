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
using backend.Logica;

namespace backend.Conversiones{

    public class Conversiones : IConversiones{

        //public int idVendedor;
        private readonly Interfaz BD;
        private readonly InterfazLogica interfazLogica;

        public Conversiones(Interfaz baseDatos, InterfazLogica interfazLogica)
        {
            this.BD = baseDatos;
            this.interfazLogica = interfazLogica;
        }


        public UsuarioBD ConvertirUsuario(Usuario usuario){

            UsuarioBD usuarioConvertido = new UsuarioBD
            {
                Id = usuario.getId(),
                Nombre = usuario.getNombre(),
                Nick_name = usuario.getNick_name(),
                Contraseña = usuario.getContraseña(),
                Email = usuario.getEmail(),
                Edad = usuario.getEdad()
            };
            return usuarioConvertido;

        }

        public CompradorBD ConvertirComprador(Comprador comprador, int userId){

            CompradorBD compradorConvertido = new CompradorBD
            {
                Id = userId,
                Limite_gasto_cents_mes = comprador.getLimite()
            };
            return compradorConvertido;

        }

        public Usuario ConvertirBDaUsuario(UsuarioBD usuarioBD){

            Usuario usuarioConvertido = new Usuario(usuarioBD.Nombre!, usuarioBD.Nick_name!, usuarioBD.Contraseña!, usuarioBD.Email!, usuarioBD.Edad);
            return usuarioConvertido;

        }

        public Producto ConvertirBDaProducto(ProductoBD productoBD){

            var datosVendedor = BD.ObtenerUsuarioPorID(productoBD.Id_usuario);     
            datosVendedor.Wait(); 
            var datosArticulo = BD.ObtenerArticuloPorID(productoBD.Id_articulo);
            datosArticulo.Wait();
            Producto productoConvertido = new Producto(productoBD.Id, productoBD.Precio_cents, productoBD.Unidades, productoBD.Id_usuario, productoBD.Id_articulo, ConvertirVendedor(datosVendedor.Result), ConvertirArticulo(datosArticulo.Result));
            return productoConvertido;

        }

         public ImagenProducto ConvertirBDaImagen(ImagenProductoBD imagenBD){

             ImagenProducto imagenProducto = new ImagenProducto(imagenBD.Id, imagenBD.Hash, imagenBD.Url, interfazLogica.devolverArticulo(imagenBD.Id_articulo));
             return imagenProducto;
         }



        public List<Producto> ConvertirListaBDaProducto(List<ProductoBD> listaProductos){
            
            List<Producto> listaProductosConvertida = new List<Producto>();
            foreach(var product in listaProductos)
            {
                listaProductosConvertida.Add(ConvertirBDaProducto(product));
            }
            return listaProductosConvertida;
        }

        public List<Usuario> ConvertirListaBDaUsuario(List<UsuarioBD> listaUsuario){
            
            List<Usuario> listaUsuarioConvertida = new List<Usuario>();
            foreach(var user in listaUsuario)
            {
                listaUsuarioConvertida.Add(ConvertirBDaUsuario(user));
            }
            return listaUsuarioConvertida;
        }

        public List<Articulo> ConvertirListaBDaArticulo(List<ArticuloBD> listaArticulos){
            
            List<Articulo> listaArticulosConvertida = new List<Articulo>();
            foreach(var product in listaArticulos)
            {
                listaArticulosConvertida.Add(ConvertirArticulo(product));
            }
            return listaArticulosConvertida;
        }

        public List<CarritoCompra> ConvertirListaBDaCarritoCompra(List<CarritoCompraBD> listaCarrito){
            
            List<CarritoCompra> listaCarritoConvertida = new List<CarritoCompra>();
            foreach(var carrito in listaCarrito)
            {
                listaCarritoConvertida.Add(ConvertirBDaCarritoCompra(carrito));
            }
            return listaCarritoConvertida;
        }
        

        public Vendedor ConvertirVendedor(UsuarioBD vendedor){

            Usuario usuarioVendedor = ConvertirBDaUsuario(vendedor);
            Vendedor vendedorConvertido =  new Vendedor(usuarioVendedor.getNombre(), usuarioVendedor.getNick_name(), usuarioVendedor.getContraseña(), usuarioVendedor.getEmail(), usuarioVendedor.getEdad());
            return vendedorConvertido;
        }

        public Articulo ConvertirArticulo(ArticuloBD articulo){

            Articulo articuloConvertido = new Articulo(articulo.Id, articulo.Nombre!, articulo.Categoria!, articulo.Edad_min, articulo.Consejos_utilizacion!, articulo.Consejos_retirada!, articulo.Origen!, articulo.Proceso_produccion!, articulo.Impacto_ambiental_social!, articulo.Contribucion_ods!, articulo.Id_usuario);
            return articuloConvertido;
        }

        public CarritoCompraBD ConvertirCarritoCompra(CarritoCompra carrito){
            CarritoCompraBD carritoConvertido = new CarritoCompraBD
            {
                Id_usuario = carrito.getId_Usuario(),
                Id_producto = carrito.getId_Producto()   
            };
            return carritoConvertido;
        }

        public CarritoCompra ConvertirBDaCarritoCompra(CarritoCompraBD carrito){

            CarritoCompra carritoConvertido = new CarritoCompra(carrito.Id_usuario, carrito.Id_producto);
            return carritoConvertido;

        }

    }

}