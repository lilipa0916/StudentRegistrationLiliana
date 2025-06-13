using Application.Interfaces;
using Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Application.DTOs;

namespace Infrastructure.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly string _connectionString;

        public MateriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Materia>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            var result = await conn.QueryAsync<Materia>(
                "sp_GetAllMaterias",
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<Materia>> GetByIdsAsync(IEnumerable<int> ids)
        {
            using var conn = new SqlConnection(_connectionString);

            var idsString = string.Join(",", ids);

            var result = await conn.QueryAsync<Materia>(
                "sp_GetMateriasByIds",
                new { Ids = idsString },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<MateriaConCompanerosDTO>> GetCompanerosPorMateriaAsync(int estudianteId)
        {
            using var conn = new SqlConnection(_connectionString);

            var result = await conn.QueryAsync<MateriaConCompanerosFlatDTO>(
                "sp_GetCompanerosPorMateria",
                new { EstudianteId = estudianteId },
                commandType: CommandType.StoredProcedure
            );

            var agrupado = result
                .GroupBy(r => r.MateriaNombre)
                .Select(g => new MateriaConCompanerosDTO
                {
                    MateriaNombre = g.Key,
                    Companeros = g.Select(x => x.CompaneroNombre).Distinct().ToList()
                });

            return agrupado;
        }
    }
}