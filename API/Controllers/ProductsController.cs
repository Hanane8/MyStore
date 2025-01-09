using Application_Layer.Commands;
using Application_Layer.Commands.ProductCommands;
using Application_Layer.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost ("add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddProductCommand(productDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccessfull)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}


