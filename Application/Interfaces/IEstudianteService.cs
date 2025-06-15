using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> GetAllAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(EstudianteCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosAsync(int estudianteId);
        Task<bool> UpdateAsync(EstudianteCreateDto dto);
    }
}
