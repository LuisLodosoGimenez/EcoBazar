


using System.Collections.ObjectModel;
using System.Reflection;

namespace backend.Models
{
    public class Articulo
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int? EdadMin { get; set; }
        public string ConsejosUtilizacion { get; set; }
        public string ConsejosRetirada { get; set; }
        public string Origen { get; set; }
        public string ProcesoProduccion { get; set; }
        public string ImpactoAmbientalSocial { get; set; }
        public string ContribucionOds { get; set; }
        public Productor Productor { get; set; }
        public ICollection<Producto> Productos { get; set; }
        public ICollection<string> ImagenesUrl { get; set; }

        public Articulo(string nombre, string categoria, int? edadMin, string consejosUtilizacion, string consejosRetirada,
        string origen, string procesoProduccion, string impacto, string ods, Productor productor)
        {

            if (productor == null) throw new Exception("El parámentro vendedor no puede quedar a NULL");
            this.Nombre = nombre;
            this.Categoria = categoria;
            this.EdadMin = edadMin;
            this.ConsejosUtilizacion = consejosUtilizacion;
            this.ConsejosRetirada = consejosRetirada;
            this.Origen = origen;
            this.ProcesoProduccion = procesoProduccion;
            this.ImpactoAmbientalSocial = impacto;
            this.ContribucionOds = ods;
            this.Productor = productor;
            this.Productos = new Collection<Producto>();
            this.ImagenesUrl = new Collection<string>();
        }
    }
}