using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;
using BibliotecaAPI.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/editoriales")]
    public class EditorialesController: ExtendedBaseController<CrearEditorialDTO, Editorial, EditorialDTO>
    {
        
        public EditorialesController(ApplicationDbContext DbContext, IMapper mapper)
            :base(DbContext, mapper, "Editoriales")
        {
        }

       

    }
}
