using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly string _connectionString;

        public EstudianteRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Estudiante>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Estudiante>("EXEC sp_GetAllEstudiantes");
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Estudiante>("EXEC sp_GetEstudianteById @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(Estudiante estudiante, List<int> materiaIds)
        {
            using var conn = new SqlConnection(_connectionString);
            var id = await conn.ExecuteScalarAsync<int>("sp_CreateEstudiante", new
            {
                estudiante.Nombre,
                estudiante.Documento
            }, commandType: CommandType.StoredProcedure);

            foreach (var m in materiaIds)
            {
                await conn.ExecuteAsync("sp_AddMateriaToEstudiante", new { EstudianteId = id, MateriaId = m },
                    commandType: CommandType.StoredProcedure);
            }

            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            var result = await conn.ExecuteAsync("EXEC sp_DeleteEstudiante @Id", new { Id = id });
            return result > 0;
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
