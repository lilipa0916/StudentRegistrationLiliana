using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IProfesorRepository _repo;
        private readonly IMapper _mapper;

        public ProfesorService(IProfesorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfesorDto>> GetAllAsync()
        {
            var profesores = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProfesorDto>>(profesores);
        }

        public async Task<ProfesorDto?> GetByIdAsync(int id)
        {
            var prof = await _repo.GetByIdAsync(id);
            return _mapper.Map<ProfesorDto?>(prof);
        }
    }
}
