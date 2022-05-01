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
    public class CategoriasController : ExtendedBaseController<CrearCategoriaDTO, Categoria, CategoriaDTO>
    {
        public CategoriasController(ApplicationDbContext DbContext, IMapper mapper)
            :base(DbContext, mapper, "Categorias")
        {
        }
    }
}
