using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Estudiante, EstudianteDto>();
            CreateMap<EstudianteCreateDto, Estudiante>();
            CreateMap<Materia, MateriaDto>();
            CreateMap<Profesor, ProfesorDto>();
        }

    }
}
