using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class CrearCategoriaDTO
    {
        [Required]
        public string Nombre { get; set; }
    }
}
