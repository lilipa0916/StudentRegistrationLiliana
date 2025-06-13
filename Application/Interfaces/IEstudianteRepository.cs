using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<Estudiante?> GetByIdAsync(int id);
        Task<int> CreateAsync(Estudiante estudiante, List<int> materiaIds);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosPorMateriaAsync(int estudianteId);
    }
}
