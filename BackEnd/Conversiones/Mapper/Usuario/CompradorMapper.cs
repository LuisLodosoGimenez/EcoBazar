using backend.Models;
using backend.ModelsSupabase;
using backend.Services;
namespace backend.Mapper
{

    public static class CompradorMapper
    {

        private static readonly Supabase.Client _supabaseClient;


        static CompradorMapper()
        {

            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);
        }


        public async static Task AñadirComprador(CompradorBD compradorBD)
        {
            await _supabaseClient
                    .From<CompradorBD>()
                    .Insert(compradorBD);
            Console.WriteLine("Comprador insertado correctamente en Supabase.");
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
                LimiteGastoMes = comprador.LimiteGastoMes
            };

        }

        public async static Task<ICollection<Producto>> ObtenerCarritoCompra(int compradorId)
        {

            var carrito = await _supabaseClient
                            .From<CarritoCompraBD>()
                            .Where(x => x.IdComprador == compradorId)
                            .Get();

            List<int> lista = new List<int>();
            lista = carrito.Models.ConvertAll((CarritoCompraBD carrito) => carrito.IdProducto);


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


            await _supabaseClient.From<CarritoCompraBD>().Insert(carritoCompraBD);
        }


        public async static Task EliminarProductoEnCarritoCompra(int compradorId, int productoId)
        {
            //todo: add to convertir
            CarritoCompraBD carritoCompraBD = new CarritoCompraBD
            {
                IdComprador = compradorId,
                IdProducto = productoId,
            };


            var result = await _supabaseClient
            .From<CarritoCompraBD>().Delete(carritoCompraBD);
        }


        public static Comprador UsuarioBDYCompradorBDAComprador(UsuarioBD usuarioBD, CompradorBD compradorBD)
        {
            var comprador = new Comprador(usuarioBD.Nombre, usuarioBD.NickName, usuarioBD.Contraseña, usuarioBD.Email);
            comprador.Id = usuarioBD.Id;
            comprador.Edad = usuarioBD.Edad;
            comprador.LimiteGastoMes = compradorBD.LimiteGastoMes;
            comprador.ImagenesUrl = usuarioBD.ImagenUrl;
            return comprador;
        }
    }
}