using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Estudiante, EstudianteDto>();
            CreateMap<EstudianteCreateDto, Estudiante>();
            CreateMap<Materia, MateriaDto>();
            CreateMap<Materia, MateriaUpdateDto>();
            CreateMap<Profesor, ProfesorDto>();

            CreateMap<EstudianteDto, Estudiante>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento));

            CreateMap<EstudianteCreateDto, Estudiante>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Documento, opt => opt.MapFrom(src => src.Documento));

        }

    }
}
