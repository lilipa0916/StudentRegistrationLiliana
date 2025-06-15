using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorDto>> GetAllAsync();
        Task<ProfesorDto?> GetByIdAsync(int id);
    }
}
