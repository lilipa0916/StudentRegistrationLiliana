using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<IEnumerable<Estudiante>> GetAllAsync();
        Task<EstudianteUpdateDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(Estudiante estudiante, List<int> materiaIds);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosPorMateriaAsync(int estudianteId);
        Task<bool> UpdateAsync(Estudiante estudiante, List<int> MateriaIds);
    }
}
