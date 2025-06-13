using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EstudianteCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public List<int> MateriaIds { get; set; } = new();
    }
}
