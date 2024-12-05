using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Requests.Commands
{
    public class AddCategoryRequest : IRequest<int>
    {
        public required string CategoryName { get; set; }
    }
}
