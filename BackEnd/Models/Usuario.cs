namespace backend.Models
{
   public class Usuario{

    private int Id;
    private string Nombre;
    private string Nick_name;
    private string Contraseña;
    private string Email;
    private int Edad;

    public Usuario(string nombre, string nick_name, string contraseña, string email, int edad){
        
        this.Nombre = nombre;
        this.Nick_name = nick_name;
        this.Contraseña = contraseña;
        this.Email = email;
        this.Edad = edad;
    }

    public int getId(){
        return this.Id;
    }

    public string getNick_name(){
        return this.Nick_name;
    }

    public string getEmail(){
        return this.Email;
    }

    public string getContraseña(){
        return this.Contraseña;
    }

    public int getEdad(){
        return this.Edad;
    }

   }
}
