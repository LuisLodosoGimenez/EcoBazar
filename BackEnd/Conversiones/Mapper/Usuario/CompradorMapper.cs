using backend.Models;
using backend.ModelsSupabase;

namespace backend.Mapper
{

    public static class CompradorMapper
    {

        public async static Task AñadirComprador(Comprador comprador)
        {


            var idUsuario = await UsuarioMapper.AñadirUsuario(CompradorMapper.CompradorAUsuarioBD(comprador));
            comprador.Id = idUsuario;
            await AñadirCompradorBD(CompradorMapper.CompradorACompradorBD(comprador));
        }


        public async static Task AñadirCompradorBD(CompradorBD compradorBD)
        {

            await SupabaseClientSingleton.getInstance()
                    .From<CompradorBD>()
                    .Insert(compradorBD);
            Console.WriteLine("Comprador insertado correctamente en Supabase.");
        }

        public async static Task<Comprador> ObtenerCompradorPorNickName(string nickName)
        {
            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.NickName == nickName)
                                .Get();



            if (!usuario.Models.Any()) throw new Exception("El NickName '" + nickName + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;


            var comprador = await SupabaseClientSingleton.getInstance()
                                    .From<CompradorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (comprador.Models.Any()) return CompradorMapper.UsuarioBDYCompradorBDAComprador(usuarioBD, comprador.Model!);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente NickName: " + nickName);

        }

        public async static Task<Comprador> ObtenerCompradorPorId(int idUsuario)
        {

            var usuario = await SupabaseClientSingleton.getInstance()
                                .From<UsuarioBD>()
                                .Where(x => x.Id == idUsuario)
                                .Get();

            if (!usuario.Models.Any()) throw new Exception("El ID '" + idUsuario + "' no corresponde a ningún usuario.");
            UsuarioBD usuarioBD = usuario.Model!;

            var comprador = await SupabaseClientSingleton.getInstance()
                                    .From<CompradorBD>()
                                    .Where(x => x.Id == usuarioBD.Id!)
                                    .Get();

            if (comprador.Models.Any()) return CompradorMapper.UsuarioBDYCompradorBDAComprador(usuarioBD, comprador.Model!);
            else throw new Exception("No se ha encontrado ningún comprador con el siguiente ID: " + idUsuario);

        }

        public static UsuarioBD CompradorAUsuarioBD(Comprador comprador)
        {

            return new UsuarioBD
            {
                Id = comprador.Id,
                Nombre = comprador.Nombre,
                NickName = comprador.NickName,
                Contraseña = comprador.Contraseña,
                Email = comprador.Email,
                Edad = comprador.Edad,
            };

        }

        public static CompradorBD CompradorACompradorBD(Comprador comprador)
        {

            return new CompradorBD
            {
                Id = comprador.Id,
                LimiteGastoCentsMes = comprador.LimiteGastoCentsMes
            };

        }

        public async static Task<ICollection<Producto>> ObtenerCarritoCompra(int compradorId)
        {

            var carrito = await SupabaseClientSingleton.getInstance()
                            .From<CarritoCompraBD>()
                            .Where(x => x.IdComprador == compradorId)
                            .Get();

            List<int> lista = new List<int>();
            lista = carrito.Models.ConvertAll((CarritoCompraBD carrito) => carrito.IdProducto);


            List<ProductoBD> productosBD = new List<ProductoBD>();
            //TODO: make a for
            foreach (var idProducto in lista)
            {

                var productos = await SupabaseClientSingleton.getInstance()
                        .From<ProductoBD>()
                        .Where(x => x.Id == idProducto)
                        .Get();

                if (productos.Model != null) { productosBD.Add(productos.Model); }


            }
            Console.WriteLine("");
            return productosBD.ConvertAll<Producto>(ProductoMapper.ConvertirProductoBDAProducto);
        }

        public async static Task AñadirProductoACarritoCompra(int compradorId, int productoId)
        {



            //todo: add to convertir
            CarritoCompraBD carritoCompraBD = new CarritoCompraBD
            {
                IdComprador = compradorId,
                IdProducto = productoId
            };


            await SupabaseClientSingleton.getInstance().From<CarritoCompraBD>().Insert(carritoCompraBD);
        }


        public async static Task EliminarProductoEnCarritoCompra(int compradorId, int productoId)
        {
            //todo: add to convertir
            CarritoCompraBD carritoCompraBD = new CarritoCompraBD
            {
                IdComprador = compradorId,
                IdProducto = productoId,
            };


            var result = await SupabaseClientSingleton.getInstance()
            .From<CarritoCompraBD>().Delete(carritoCompraBD);
        }


        public static Comprador UsuarioBDYCompradorBDAComprador(UsuarioBD usuarioBD, CompradorBD compradorBD)
        {
            var comprador = new Comprador(usuarioBD.Nombre, usuarioBD.NickName, usuarioBD.Contraseña, usuarioBD.Email);
            comprador.Id = usuarioBD.Id;
            comprador.Edad = usuarioBD.Edad;
            comprador.LimiteGastoCentsMes = compradorBD.LimiteGastoCentsMes;
            comprador.ImagenesUrl = usuarioBD.ImagenUrl;
            return comprador;
        }

        public static async Task EliminarCarritoCompra(int id_comprador)
        {
            await SupabaseClientSingleton.getInstance()
            .From<CarritoCompraBD>().Where(x => x.IdComprador == id_comprador).Delete();
        }


    }
}