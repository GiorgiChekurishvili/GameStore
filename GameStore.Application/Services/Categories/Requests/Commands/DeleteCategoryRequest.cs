using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Requests.Commands
{
    public class DeleteCategoryRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
