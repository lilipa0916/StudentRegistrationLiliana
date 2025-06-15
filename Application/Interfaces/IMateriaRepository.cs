using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<MateriaDto>> GetAllAsync();
        Task<IEnumerable<MateriaDto>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosPorMateriaAsync(int estudianteId);
    }
}
