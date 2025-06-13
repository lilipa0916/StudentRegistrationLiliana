using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
