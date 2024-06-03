using backend.Mapper;
using backend.Models;
using static backend.Controllers.ApiController;

namespace backend.MetodoFabrica
{
    public class CompradorFabrica : UsuarioFabrica
    {


        public override Usuario CrearUsuario(Registro registro)
        {
            Comprador comprador = new Comprador(registro.Nombre!, registro.NickName!, registro.Contraseña!, registro.Email!);
            comprador.Edad = registro.Edad;
            comprador.LimiteGastoCentsMes = registro.LimiteGasto;
            return comprador;

        }

        public override async Task<Usuario> ObtenerUsuario(int idUsuario)
        {
            Comprador comprador = await CompradorMapper.ObtenerCompradorPorId(idUsuario);
            comprador.CarritoCompra = await CompradorMapper.ObtenerCarritoCompra((int)comprador.Id!);
            AplicarDescuentosACarritoCompra(comprador.CarritoCompra);
            comprador.Pedidos = await PedidoMapper.ObtenerPedidosComprador((int)comprador.Id!);
            return comprador;
        }

        public override async Task<Usuario> ObtenerUsuario(string nickName)
        {
            Comprador comprador = await CompradorMapper.ObtenerCompradorPorNickName(nickName);
            comprador.CarritoCompra = await CompradorMapper.ObtenerCarritoCompra((int)comprador.Id!);
            AplicarDescuentosACarritoCompra(comprador.CarritoCompra);
            comprador.Pedidos = await PedidoMapper.ObtenerPedidosComprador((int)comprador.Id!);
            return comprador;
        }

        private void AplicarDescuentosACarritoCompra(ICollection<Producto> carritoCompra)
        {
            foreach (Producto producto in carritoCompra)
            {
                producto.DescuentoAplicado = DeterminarDescuento(producto);
                producto.AplicarDescuento();
            }
        }

        private IDescuento? DeterminarDescuento(Producto producto)
        {
            int mesActual = DateTime.Now.Month;
            IDescuento? descuento = null;

            switch (mesActual)
            {
                case 1:
                    descuento = new DescuentoInvierno();
                    break;

                case 4:
                    descuento = new DescuentoPrimavera();
                    break;

                case 6:
                    descuento = new DescuentoVerano();
                    break;

                case 11:
                    descuento = new DescuentoOtonyo();
                    break;

            }
            return descuento;
        }
    }
}