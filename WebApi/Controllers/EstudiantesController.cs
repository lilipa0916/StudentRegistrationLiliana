using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _service;

        public EstudiantesController(IEstudianteService service)
        {
            _service = service;
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
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
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
    }
}
