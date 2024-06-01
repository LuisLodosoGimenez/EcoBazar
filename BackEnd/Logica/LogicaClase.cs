
using backend.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using backend.ModelsSupabase;
using System.Collections.ObjectModel;
using backend.Mapper;

namespace backend.Logica
{
    public class LogicaClase : InterfazLogica
    {
        public LogicaClase() { }



        public async Task RegistrarComprador(Comprador comprador)
        {
            if (UsuarioMapper.ExisteNickNameEnUsuario(comprador.NickName).Result)
            {
                throw new Exception("NickName '" + comprador.NickName + "' ya en uso.");

            }
            else if (UsuarioMapper.ExisteEmailEnUsuario(comprador.Email).Result)
            {
                throw new Exception("Email '" + comprador.Email + "' ya en uso.");
            }
            else
            {
                var idUsuario = await UsuarioMapper.AñadirUsuario(CompradorMapper.CompradorAUsuarioBD(comprador));
                comprador.Id = idUsuario;
                await CompradorMapper.AñadirComprador(CompradorMapper.CompradorACompradorBD(comprador));

            }
        }




        public async Task<object> IniciarSesion(string nickName, string contraseña)
        {


            var objeto = await UsuarioMapper.ObtenerUsuarioPorNickName(nickName);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;



                if (comprador!.Contraseña != contraseña) throw new Exception("Contraseña incorrecta");
                ICollection<Producto> productos = await CompradorMapper.ObtenerCarritoCompra((int)comprador.Id!);

                comprador.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("ERROR DESCONOCIDOOO");
        }



        public async Task<IList<Producto>> ObtenerProductosPorCategoria(string categoria)
        {
            List<Articulo> articulos = await ArticuloMapper.ObtenerArticulosPorCategoria(categoria);


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

            var result = ProductoMapper.ObtenerProductosPorIDArticulo((int)articulo.Id!, articulo);
            result.Wait();

            List<Producto> productos = result.Result;

            articulo.Productos = productos;
            return articulo;

        }



        private Producto? ObtenerProductoMasBaratoDeArticulo(Articulo articulo)
        {
            List<Producto> productosOrdenados = articulo.Productos.OrderBy(producto => producto.PrecioCents).ToList();

            if (productosOrdenados.Count != 0) return productosOrdenados.First();
            else return null;
        }


        public async Task<Comprador> AñadirProductoACarritoCompra(int idComprador, int idProducto)
        {


            var objeto = await UsuarioMapper.ObtenerUsuarioPorId(idComprador);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;
                await CompradorMapper.AñadirProductoACarritoCompra((int)comprador!.Id!, idProducto);
                ICollection<Producto> productos = await CompradorMapper.ObtenerCarritoCompra(idComprador);
                
                foreach(Producto producto in productos){
                    producto.descuentoAplicado = DeterminarDescuento(producto);
                    producto.PrecioCents = producto.CalcularDescuento();
                }

                comprador!.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("EL ID DE USUARIO NO ES DE COMPRADOR");
        }

        private IDescuento DeterminarDescuento(Producto producto){

            int mesActual = DateTime.Now.Month;
            IDescuento descuento = new SinDescuento();

            switch(mesActual){
                case 1:
                    descuento = new DescuentoInvierno();
                    break;
               
                case 4:
                    descuento = new DescuentoPrimavera();
                    break;
               
                case 7:
                    descuento = new DescuentoVerano();
                    break;
                
                case 11:
                    descuento = new DescuentoOtonyo();
                    break;
            
            }

            return descuento;

        }
        

        public async Task<Comprador> EliminarProductoEnCarritoCompra(int idComprador, int idProducto)
        {


            var objeto = await UsuarioMapper.ObtenerUsuarioPorId(idComprador);

            if (objeto.GetType() == Type.GetType("backend.Models.Comprador"))
            {

                Comprador? comprador = objeto as Comprador;
                await CompradorMapper.EliminarProductoEnCarritoCompra((int)comprador!.Id!, idProducto);
                ICollection<Producto> productos = await CompradorMapper.ObtenerCarritoCompra(idComprador);

                comprador!.CarritoCompra = productos;

                return comprador;

            }
            //todo: make it correctly

            throw new Exception("EL ID DE USUARIO NO ES DE COMPRADOR");
        }

    }

}