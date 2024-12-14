using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Categories.Requests.Commands;
using GameStore.Application.Services.Categories.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;


namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoriesRetrieveDTO>>> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesRequest());
            return Ok(categories);
        }

        [HttpGet("GetAllGamesByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<GameByCategoryRetrieveDTO>>> GetAllGamesByCategory(int categoryId)
        {
            var categories = await _mediator.Send(new GetAllGamesByCategoryRequest { CategoryId = categoryId });
            return Ok(categories);
        }

        [Authorize]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string CategoryName)
        {
            try
            {
                await _mediator.Send(new AddCategoryRequest { CategoryName = CategoryName });
                return Ok();
            }
            catch(BadRequestException ex)
            {
                return BadRequest(new {error = ex.Message});
            }
        }

        [Authorize]
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, string CategoryName)
        {
            try
            {
                CategoryUpdateDTO updateDTO = new CategoryUpdateDTO { CategoryName = CategoryName, Id = id };
                await _mediator.Send(new UpdateCategoryRequest { Category = updateDTO });
                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [Authorize]
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           
            await _mediator.Send(new DeleteCategoryRequest { Id = id });
            return Ok();
            
            
        }
    }
}
