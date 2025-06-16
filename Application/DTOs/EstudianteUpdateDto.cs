using System.IO.Pipes;

namespace Application.DTOs
{
    public class EstudianteUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }

        public List<MateriaUpdateDto> Materias { get; set; }

    }
}
