using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MateriaUpdateDto
    {
        public int MateriaId { get; set; }
        public string NombreMateria { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public int ProfesorId { get; set; }
        public string ProfesorNombre { get; set; }


    }
}
