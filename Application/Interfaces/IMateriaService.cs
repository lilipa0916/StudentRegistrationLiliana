using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMateriaService
    {
        Task<IEnumerable<MateriaDto>> GetAllAsync();
        Task <IEnumerable<MateriaDto>> GetByIdsAsync(List<int> materiaIds);
    }
}
