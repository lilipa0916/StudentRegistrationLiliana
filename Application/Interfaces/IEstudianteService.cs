using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> GetAllAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(EstudianteCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosAsync(int estudianteId);
    }
}
