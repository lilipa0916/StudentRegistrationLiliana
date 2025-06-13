using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class MateriaConCompanerosFlatDTO
    {
        public string MateriaNombre { get; set; } = "";
        public string CompaneroNombre { get; set; } = "";
    }

    public class MateriaConCompanerosDTO
    {
        public string MateriaNombre { get; set; } = "";
        public List<string> Companeros { get; set; } = new();
    }
}
