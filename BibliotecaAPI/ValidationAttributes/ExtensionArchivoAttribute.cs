using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class ExtensionArchivoAttribute: ValidationAttribute
    {
        private readonly string[] _TiposValidos;

        public ExtensionArchivoAttribute(string[] tiposValidos)
        {
            _TiposValidos = tiposValidos;
        }

        public ExtensionArchivoAttribute(TipoArchivo tipoArchivo)
        {
            if (tipoArchivo == TipoArchivo.Image)
            {
                _TiposValidos = new[] { "image/png", "image/jpg", "image/jpeg", "image/gif"}; 
            }
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            
            if (formFile != null)
            {
                if (!_TiposValidos.Contains(formFile.ContentType))
                {
                    return new ValidationResult($"Los tipos válidos son {string.Join(",", _TiposValidos)}");
                }
            }

            return ValidationResult.Success; 
        }
    }
}
