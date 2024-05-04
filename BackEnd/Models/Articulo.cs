


namespace backend.Models
{
    public class Articulo{
        private int Id;
        private string Nombre;
        private string Categoria;
        private int Edad_min;
        private string Consejos_utilizacion;
        private string Consejos_retirada;
        private string Origen;
        private string Proceso_produccion;
        private string Impacto_ambiental_social;
        private string Contribucion_ods;
        private int Id_usuario;
        
        public Articulo(int id, string nombre, string categoria, int edad_min, string consejos_utilizacion, string consejos_retirada,string origen, string proceso_produccion, string impacto, string ods, int id_usuario){
            this.Id = id;
            this.Nombre = nombre;
            this.Categoria = categoria;
            this.Edad_min = edad_min;
            this.Consejos_utilizacion = consejos_utilizacion;
            this.Consejos_retirada = consejos_retirada;
            this.Origen = origen;
            this.Proceso_produccion = proceso_produccion;
            this.Impacto_ambiental_social = impacto;
            this.Contribucion_ods = ods;
            this.Id_usuario = id_usuario;
        }

        public int getId(){
            return this.Id;
        }

        public string getNombre(){
            return this.Nombre;
        }

        public string getCategoria(){
            return this.Categoria;
        }

        public string getConsejos(){
            return this.Consejos_utilizacion;
        }

        public string getConsejosRetirada(){
            return this.Consejos_retirada;
        }

        public string getOrigen(){
            return this.Origen;
        }

        public string getProcesoProduccion(){
            return this.Proceso_produccion;
        }

        public string getImpacto(){
            return this.Impacto_ambiental_social;
        }

        public string getODS(){
            return this.Contribucion_ods;
        }
    }

}