using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriasController(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaDTO>>> Get()
        {
            var categorias = await _dbContext.Categoria.ToListAsync();

            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoriaDTO>(categoria);
        }


        [HttpPost]
        public async Task<ActionResult> Post(CrearCategoriaDTO crearCategoriaDTO)
        {
            var categoria = _mapper.Map<Categoria>(crearCategoriaDTO);

            _dbContext.Add(categoria);
            await _dbContext.SaveChangesAsync();

            var dto = _mapper.Map<CategoriaDTO>(categoria);

            return new CreatedAtRouteResult("GetCategoria", new { Id = categoria.Id }, dto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, CrearCategoriaDTO crearCategoriaDTO)
        {
            var categoria = await _dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            _mapper.Map(crearCategoriaDTO, categoria);

            _dbContext.Entry(categoria).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _dbContext.Categoria.FirstOrDefaultAsync(c => c.Id == id);


            if (categoria == null)
            {
                return NotFound();
            }

            _dbContext.Entry(categoria).State = EntityState.Deleted;
            
            await _dbContext.SaveChangesAsync();

            return NoContent();

        }
    }
}
