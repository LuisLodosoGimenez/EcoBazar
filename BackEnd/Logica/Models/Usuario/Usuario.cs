using backend.ModelsSupabase;

namespace backend.Models
{
   public class Usuario{

    public int? Id{get;set;}
    public string Nombre{get;set;}
    public string Nick_name{get;set;}
    public string Contraseña{get;set;}
    public string Email{get;set;}
    public int? Edad{get;set;}
    public string? ImagenesUrl{get;set;}



    public Usuario(string nombre, string nick_name, string contraseña, string email){
        
        this.Nombre = nombre;
        this.Nick_name = nick_name;
        this.Contraseña = contraseña;
        this.Email = email;
    }
   }
}
