using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly string _connectionString;

        public MateriaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<MateriaDto>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            var result = await conn.QueryAsync<MateriaDto>(
                "sp_GetAllMaterias",
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<MateriaDto>> GetByIdsAsync(IEnumerable<int> ids)
        {
            using var conn = new SqlConnection(_connectionString);

            var idsString = string.Join(",", ids);

            var result = await conn.QueryAsync<MateriaDto>(
                "sp_GetMateriasByIds",
                new { Ids = idsString,
                        
                        },
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