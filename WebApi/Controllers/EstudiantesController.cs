using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _service;
        private readonly IMateriaService _serviceMat;

        public EstudiantesController(IEstudianteService service, IMateriaService serviceMat)
        {
            _service = service;
            _serviceMat = serviceMat;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estudiantes = await _service.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estudiante = await _service.GetByIdAsync(id);
            return estudiante == null ? NotFound() : Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EstudianteCreateDto dto)
        {
            try
            {
                if (dto.MateriaIds.Count != 3)
                {
                    return BadRequest("Un estudiante solo puede seleccionar 3 materias.");
                }

                var materias = await _serviceMat.GetByIdsAsync(dto.MateriaIds);
                var profesores = materias.GroupBy(m => m.ProfesorId);
                if (profesores.Any(g => g.Count() > 1))
                {
                    return BadRequest("No puedes seleccionar 2 materias con el mismo profesor.");
                }


                var id = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado.", error = ex.Message });

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("{id}/companeros")]
        public async Task<IActionResult> GetCompaneros(int id)
        {
            var result = await _service.GetCompanerosAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EstudianteCreateDto dto)
        {
            if (dto.MateriaIds.Count != 3)
            {
                return BadRequest("Un estudiante solo puede seleccionar 3 materias.");
            }

            var materias = await _serviceMat.GetByIdsAsync(dto.MateriaIds);
            var profesores = materias.GroupBy(m => m.ProfesorId);
            if (profesores.Any(g => g.Count() > 1))
            {
                return BadRequest("No puedes seleccionar 2 materias con el mismo profesor.");
            }


            if (id != dto.Id)
            {
                return BadRequest("El ID del estudiante no coincide.");
            }

            var estudiante = await _service.GetByIdAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(dto);
            return NoContent();
        }


    }
}
