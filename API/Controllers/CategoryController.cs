using Application_Layer.Queries.CategoryQueries.GetAllCategories;
using Application_Layer.Queries.CategoryQueries.GetCategoryById;
using Application_Layer.Queries.CategoryQueries.GetCategoryByName;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccessfull)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var query = new GetAllCategoryQuery();
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccessfull)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name, CancellationToken cancellationToken)
        {
            var query = new GetCategoryByNameQuery(name);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccessfull)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
    


