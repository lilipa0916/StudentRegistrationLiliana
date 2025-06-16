using Application.DTOs;
using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

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

        public async Task<EstudianteUpdateDto?> GetByIdAsync(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            try
            {


                var query = "EXEC sp_GetEstudianteById @Id";

                var result = await conn.QueryMultipleAsync(query, new { Id = id });

                // Leer el primer conjunto de resultados (el estudiante)
                var estudiante = await result.ReadFirstOrDefaultAsync<EstudianteUpdateDto>();

                if (estudiante == null)
                {
                    return null; // Si no hay estudiante, devolver null
                }

                // Leer el segundo conjunto de resultados (las materias)
                var materias = await result.ReadAsync<MateriaUpdateDto>();

                // Asignar las materias al estudiante
                estudiante.Materias = materias.ToList();

                return estudiante;
            }
            catch (Exception ex)
            {

                throw;
            }
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
            return result == -1;
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
        public async Task<bool> UpdateAsync(Estudiante dto, List<int> MateriaIds)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync(); // Asegúrate de que la conexión se abra correctamente

            // Transacción para manejar la actualización y la relación de materias
            using (var transaction = await conn.BeginTransactionAsync())  // Usar BeginTransactionAsync para operaciones asíncronas
            {
                try
                {
                    // Ejecutar el procedimiento almacenado para actualizar el estudiante
                    var result = await conn.ExecuteAsync("sp_UpdateEstudiante",
                        new
                        {
                            Id = dto.Id,
                            Nombre = dto.Nombre,
                            Documento = dto.Documento,
                            MateriaIds = string.Join(",", MateriaIds) // Si el procedimiento espera una cadena, este es el formato correcto
                        },
                        commandType: CommandType.StoredProcedure,
                        transaction: transaction);

                    // Si el resultado no es el esperado, lanzamos un error
                    if (result <= 0)
                    {
                        throw new Exception("No se actualizó el estudiante correctamente.");
                    }

                    // Commit de la transacción
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Hacer rollback en caso de error
                    await transaction.RollbackAsync();

                    // Loguear el error para mayor claridad
                    Console.WriteLine($"Error en la actualización del estudiante: {ex.Message}");
                    // Puedes agregar aquí un sistema de logs como NLog, Serilog, etc. para mejorar el rastreo de errores

                    return false;
                }
            }
        }
    }
}
