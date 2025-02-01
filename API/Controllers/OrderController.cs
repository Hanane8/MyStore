using Application_Layer.Commands.OrderCommands;
using Application_Layer.Queries.OrderQueries;
using Application_Layer.Queries.OrderQueries.GetOrderById;
using Application_Layer.Queries.OrderQueries.GetOrderByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        //[Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            { 
                var result = await _mediator.Send(command);

                if (result.IsSuccessfull)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var query = new GetOrderByIdQuery(id); 
                var result = await _mediator.Send(query); 

                if (result.IsSuccessfull)
                {
                    return Ok(result); 
                }
                else
                {
                    return NotFound(result); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new GetOrdersByUserIdQuery(userId));

                if (result.IsSuccessfull && result.Data != null && result.Data.Any())
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound(new { message = "No orders found for the given user." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", error = ex.Message });
            }
        }
    }
}
