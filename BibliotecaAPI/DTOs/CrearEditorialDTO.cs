using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class CrearEditorialDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
