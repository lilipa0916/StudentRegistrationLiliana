using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class EstudianteCreateValidator : AbstractValidator<EstudianteCreateDto>
    {
        public EstudianteCreateValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("El documento es obligatorio")
                .MaximumLength(20);

            RuleFor(x => x.MateriaIds)
                .Must(x => x.Count == 3)
                .WithMessage("Debe seleccionar exactamente 3 materias");
        }
    }
}
