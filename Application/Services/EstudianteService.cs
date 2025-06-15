using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;


namespace Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMateriaRepository _materiaRepository;

        public EstudianteService(IEstudianteRepository repo, IMapper mapper, IMateriaRepository materiaRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _materiaRepository = materiaRepository;
        }

        public async Task<IEnumerable<EstudianteDto>> GetAllAsync()
        {
            var estudiantes = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
        }

        public async Task<EstudianteDto?> GetByIdAsync(int id)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            return estudiante == null ? null : _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<int> CreateAsync(EstudianteCreateDto dto)
        {
            if (dto.MateriaIds.Count > 3)
            {
                throw new Exception("Un estudiante solo puede seleccionar hasta 3 materias.");
            }

            var materias = await _materiaRepository.GetByIdsAsync(dto.MateriaIds);
            var profesores = materias.GroupBy(m => m.Id);
            if (profesores.Any(g => g.Count() > 1))
                throw new Exception("No puedes seleccionar 2 materias con el mismo profesor.");

            var entity = _mapper.Map<Estudiante>(dto);
            return await _repo.CreateAsync(entity, dto.MateriaIds);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
        public async Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosAsync(int estudianteId)
        {
            return await _repo.GetCompanerosPorMateriaAsync(estudianteId);
        }

        public async Task<bool> UpdateAsync(EstudianteCreateDto dto)
        {
            if (dto.MateriaIds.Count > 3)
            {
                throw new Exception("Un estudiante solo puede seleccionar hasta 3 materias.");
            }

            var materias = await _materiaRepository.GetByIdsAsync(dto.MateriaIds);
            var profesores = materias.GroupBy(m => m.Id);
            if (profesores.Any(g => g.Count() > 1))
                throw new Exception("No puedes seleccionar 2 materias con el mismo profesor.");

            var estudiante = _mapper.Map<Estudiante>(dto);

            return await _repo.UpdateAsync(estudiante, dto.MateriaIds); 

        }
    }
}

