using Application.DTOs;
using Application.Interfaces;
using AutoMapper;

namespace Application.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly IMateriaRepository _repo;
        private readonly IMapper _mapper;

        public MateriaService(IMateriaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MateriaDto>> GetAllAsync()
        {
            var materias = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MateriaDto>>(materias);
        }

        public async Task<IEnumerable<MateriaDto>> GetByIdsAsync(List<int> materiaIds)
        {
            var materias = await _repo.GetByIdsAsync(materiaIds);
            return _mapper.Map<IEnumerable<MateriaDto>>(materias);
        }
    }
}
