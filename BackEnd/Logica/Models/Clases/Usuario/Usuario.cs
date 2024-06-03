using backend.ModelsSupabase;

namespace backend.Models
{
    public class Usuario
    {

        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string NickName { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public int? Edad { get; set; }
        public string? ImagenesUrl { get; set; }



        public Usuario(string nombre, string nickName, string contraseña, string email)
        {

            this.Nombre = nombre;
            this.NickName = nickName;
            this.Contraseña = contraseña;
            this.Email = email;
        }
    }
}
