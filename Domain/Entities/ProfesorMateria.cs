using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProfesorMateria
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public int MateriaId { get; set; }
    }
}
