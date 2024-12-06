﻿using GameStore.Application.DTOs.CategoryDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Requests.Commands
{
    public class UpdateCategoryRequest : IRequest<Unit>
    {
        public CategoryDTO? Category { get; set; }
    }
}
