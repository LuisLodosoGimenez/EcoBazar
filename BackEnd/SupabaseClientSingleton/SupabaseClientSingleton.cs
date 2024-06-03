public sealed class SupabaseClientSingleton
{
    private static Supabase.Client? _supabaseClient = null;

    private SupabaseClientSingleton()
    {
    }

    public static Supabase.Client getInstance()
    {
        if (_supabaseClient == null)
        {
            var supabaseUrl = "https://llpjnoklflyjokandifh.supabase.co";
            var supabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImxscGpub2tsZmx5am9rYW5kaWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTM5Nzc0MzQsImV4cCI6MjAyOTU1MzQzNH0.IeBIVRWX_9LEGvCB7KQVntdIP3arB0ZF3SVOVJbktug";

            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            _supabaseClient = new Supabase.Client(supabaseUrl!, supabaseKey, options);

            InitializeDB();
        }
        return _supabaseClient;
    }

    private static void InitializeDB()
    {
        Console.WriteLine("Iniciando la conexión a Supabase...");
        _supabaseClient!.InitializeAsync();
        Console.WriteLine("Conexión a Supabase completada.");
    }
}