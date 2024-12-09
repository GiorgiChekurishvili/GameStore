﻿using GameStore.Application.DTOs.LibraryDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Library.Requests.Queries
{
    public class GetLibraryGameByIdRequest : IRequest<LibraryRetrieveDTO>
    {
        public int Id { get; set; }
    }
}
