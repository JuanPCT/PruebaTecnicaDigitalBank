using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaAPI.Model
{
    public class UsuarioData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El sexo es requerido")]
        [RegularExpression("^[MFO]$", ErrorMessage = "El sexo debe ser M, F u O")]
        public string Sexo { get; set; } = string.Empty;
    }
}
