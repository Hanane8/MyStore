using Application_Layer.Commands;
using Application_Layer.Commands.ProductCommands;
using Application_Layer.Commands.ProductCommands.AddProductCommands;
using Application_Layer.Commands.ProductCommands.DeleteProductCommand;
using Application_Layer.Commands.ProductCommands.UpdateProductCommands;
using Application_Layer.DTO.ProductDto;
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
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDTO updateProductDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateProductCommand(id, updateProductDto);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound(new { Message = "Product not found" });
            }

            return Ok(new { Message = "Product updated successfully" });
        }

        [HttpDelete("Delete/{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            if (!result)
            {
                return NotFound(new { Message = $"Product with Id {id} not found." });
            }

            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}


