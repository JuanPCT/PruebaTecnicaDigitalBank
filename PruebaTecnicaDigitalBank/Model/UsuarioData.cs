namespace PruebaTecnicaDigitalBank.Model
{
    public class UsuarioData
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;
        public string Sexo { get; set; } = string.Empty;
    }
}
