


using System.Collections.ObjectModel;
using System.Reflection;

namespace backend.Models
{
    public class Articulo{
        public int? Id {get;set;}
        public string Nombre{get;set;}
        public string Categoria{get;set;}
        public int? Edad_min{get;set;}
        public string Consejos_utilizacion{get;set;}
        public string Consejos_retirada{get;set;}
        public string Origen{get;set;}
        public string Proceso_produccion{get;set;}
        public string Impacto_ambiental_social{get;set;}
        public string Contribucion_ods{get;set;}
        public Productor Productor{get;set;}
        public ICollection<Producto> Productos{get;set;}
        public ICollection<string> ImagenesUrl {get;set;}
        
        public Articulo(string nombre, string categoria, int? edad_min, string consejos_utilizacion, string consejos_retirada,
        string origen, string proceso_produccion, string impacto, string ods, Productor productor){
            this.Nombre = nombre;
            this.Categoria = categoria;
            this.Edad_min = edad_min;
            this.Consejos_utilizacion = consejos_utilizacion;
            this.Consejos_retirada = consejos_retirada;
            this.Origen = origen;
            this.Proceso_produccion = proceso_produccion;
            this.Impacto_ambiental_social = impacto;
            this.Contribucion_ods = ods;
            this.Productor = productor;
            this.Productos = new Collection<Producto>();
            this.ImagenesUrl = new Collection<string>();
        }
    }
}