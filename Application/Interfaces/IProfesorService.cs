﻿using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorDto>> GetAllAsync();
        Task<ProfesorDto?> GetByIdAsync(int id);
    }
}
