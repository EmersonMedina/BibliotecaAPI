using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ExtendedBaseController<CrearAutorDTO, Autor, AutorDTO>
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IMapper _Mapper;
        private readonly IAlmacenadorArchivos _AlmacenadorArchivos;

        public AutoresController(ApplicationDbContext applicationDbContext, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos)
            : base(applicationDbContext, mapper, "Autores")
        {
            _DbContext = applicationDbContext;
            _Mapper = mapper;
            _AlmacenadorArchivos = almacenadorArchivos;
        }

        public async override Task<ActionResult> Post([FromForm] CrearAutorDTO crearEntidadDTO)
        {
            var entity = _Mapper.Map<Autor>(crearEntidadDTO);

            if (crearEntidadDTO.Foto != null )
            {
                string fotoUrl = await GuardarFoto(crearEntidadDTO.Foto);
                entity.Foto = fotoUrl; 
            }

            _DbContext.Add(entity);

            await _DbContext.SaveChangesAsync();

            var dto = _Mapper.Map<AutorDTO>(entity);

            return new CreatedAtActionResult(nameof(Get), "Autores", new { id = entity.Id }, dto); 
        }

        public async override Task<ActionResult> Put(int id, [FromForm] CrearAutorDTO crearEntidadDTO)
        {
            var entity = await _DbContext.Autores.FirstOrDefaultAsync(a => a.Id == id); 
            
            if (entity == null)
            {
                return NotFound(); 
            }

            _Mapper.Map(crearEntidadDTO, entity); 

            if (crearEntidadDTO.Foto != null)
            {
                if (!string.IsNullOrEmpty(entity.Foto))
                {
                    await _AlmacenadorArchivos.Borrar(entity.Foto, ConstantesDeAplicacion.ContenedoresDeArchivos.ContenedorAutores); 
                }

                string fotoUrl = await GuardarFoto(crearEntidadDTO.Foto);

                entity.Foto = fotoUrl;
            }

            _DbContext.Entry(entity).State = EntityState.Modified;

            await _DbContext.SaveChangesAsync();

            return NoContent();




        }

        private async Task<string> GuardarFoto(IFormFile foto)
        {
            using var stream = new MemoryStream();

            await foto.CopyToAsync(stream);

            var fileBytes = stream.ToArray();

            return await _AlmacenadorArchivos.Crear(fileBytes, foto.ContentType, Path.GetExtension(foto.FileName), ConstantesDeAplicacion.ContenedoresDeArchivos.ContenedorAutores, Guid.NewGuid().ToString()); 
        }

    }
}
