using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.ValidationAttributes
{
    public class PesoArchivoAttribute: ValidationAttribute
    {
        private readonly double _pesoArchivoKb;
        public PesoArchivoAttribute(double pesoArchivoKb)
        {
            _pesoArchivoKb = pesoArchivoKb;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile; 

            if (formFile != null)
            {
                if ((formFile.Length/1024) > _pesoArchivoKb)
                {
                    return new ValidationResult($"El peso máximo para el archivo que enviaste es de: {_pesoArchivoKb} KB, sin embargo has enviado un archivo con {formFile.Length/1024} KB"); 
                }
            }

            return ValidationResult.Success; 
        }

    }
}
