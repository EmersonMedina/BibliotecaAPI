using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //---------- Categorias -------------//
            CreateMap<Categoria, CategoriaDTO>()
                .ReverseMap();

            CreateMap<CrearCategoriaDTO, Categoria>()
            .ReverseMap();

            //---------- Editoriales -------------//

            CreateMap<Editorial, EditorialDTO>()
                .ReverseMap();

            CreateMap<CrearEditorialDTO, Editorial>()
            .ReverseMap();

        }
    }
}
