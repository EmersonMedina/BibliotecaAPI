using BibliotecaAPI.ValidationAttributes;

namespace BibliotecaAPI.DTOs
{
    public class CrearAutorDTO
    {
        public string Nombre { get; set; }
        [ExtensionArchivo(TipoArchivo.Image)] 
        [PesoArchivo(1024)]
        public IFormFile Foto { get; set; }
    }
}
