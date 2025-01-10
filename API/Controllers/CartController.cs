using Application_Layer.Commands.CartCommands.AddToCartCommands;
using Application_Layer.Commands.CartCommands.UpdateCartCommands;
using Application_Layer.DTO.CartDTO;
using Application_Layer.DTO.CartItemDTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDTO addToCartDto)
        {
            if (addToCartDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            var command = new AddToCartCommand(addToCartDto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartItemDTO updateCartItemDto)
        {
            if (updateCartItemDto == null)
            {
                return BadRequest("Invalid request payload.");
            }

            var command = new UpdateCartCommand(updateCartItemDto);
            var result = await _mediator.Send(command);

            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}

