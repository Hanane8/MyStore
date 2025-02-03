using Application_Layer.Commands.CartCommands.AddToCartCommands;
using Application_Layer.DTO.CartDTO;
using Application_Layer.DTO.CartItemDTO;
using Application_Layer.Queries.CartQueries.GetCartByUserId;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }

            OperationResult<CartDto> result = await _mediator.Send(new GetCartByUserIdQuery(userId));

            if (!result.IsSuccessfull)
            {
                return NotFound(result.Message);
            }

            return Ok(result);
        }
    }
}

