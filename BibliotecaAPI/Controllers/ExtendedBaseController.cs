using AutoMapper;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    public class ExtendedBaseController<TCreation, TEntity, TDTO>: ControllerBase
        where TEntity:class, IHaveId
    {
        private readonly ApplicationDbContext _DbContext;
        private readonly IMapper _Mapper;
        private readonly string _ControllerName;

        public ExtendedBaseController(ApplicationDbContext DbContext, IMapper mapper, string controllerName)
        {
            _DbContext = DbContext;
            _Mapper = mapper;
            _ControllerName = controllerName;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TDTO>>> Get()
        {
            var entidades = await _DbContext.Set<TEntity>().ToListAsync();

            return _Mapper.Map<List<TDTO>>(entidades);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDTO>> Get(int id)
        {
            var entidad = await _DbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return _Mapper.Map<TDTO>(entidad);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post(TCreation crearEntidadDTO)
        {
            var entidad = _Mapper.Map<TEntity>(crearEntidadDTO);

            _DbContext.Add(entidad);
            await _DbContext.SaveChangesAsync();

            var dto = _Mapper.Map<TDTO>(entidad);

            return new CreatedAtActionResult(nameof(Get), _ControllerName,  new { Id = entidad.Id }, dto);

        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, TCreation crearEntidadDTO)
        {
            var entidad = await _DbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            _Mapper.Map(crearEntidadDTO, entidad);

            _DbContext.Entry(entidad).State = EntityState.Modified;

            await _DbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var entidad = await _DbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);


            if (entidad == null)
            {
                return NotFound();
            }

            _DbContext.Entry(entidad).State = EntityState.Deleted;

            await _DbContext.SaveChangesAsync();

            return NoContent();

        }


    }
}
