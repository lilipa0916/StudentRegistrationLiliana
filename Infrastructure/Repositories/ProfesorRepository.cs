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
    public class ProfesorRepository : IProfesorRepository
    {
        private readonly string _connectionString;

        public ProfesorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Profesor>> GetAllAsync()
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryAsync<Profesor>(
                "sp_GetAllProfesores",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Profesor?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            return await conn.QueryFirstOrDefaultAsync<Profesor>(
                "sp_GetProfesorById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
