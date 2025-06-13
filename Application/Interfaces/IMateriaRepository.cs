using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMateriaRepository
    {
        Task<IEnumerable<Materia>> GetAllAsync();
        Task<IEnumerable<Materia>> GetByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosPorMateriaAsync(int estudianteId);
    }
}
