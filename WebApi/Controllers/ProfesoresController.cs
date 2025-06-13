using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _service;

        public ProfesoresController(IProfesorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var profesores = await _service.GetAllAsync();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var profesor = await _service.GetByIdAsync(id);
            return profesor == null ? NotFound() : Ok(profesor);
        }
    }
}
