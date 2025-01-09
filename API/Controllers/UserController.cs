using Application_Layer.Commands.UserCommands.RegisterUser;
using Application_Layer.DTO.UserDto;
using Application_Layer.Queries.UserQueries.LoginUser;
using Application_Layer.Queries.UserQueries.LogoutUser;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RegisterUserCommandValidator _validator;

        public UserController(IMediator mediator, RegisterUserCommandValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            if (result.IsSuccessfull)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _mediator.Send(new LoginUserQuery(loginUserDTO));                               

            if (result.IsSuccessfull)
            {
                return Ok(new {result.Message, Token = result.Data });
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            var tokenAuth = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(tokenAuth) || !tokenAuth.StartsWith("Bearer "))
            {
                return BadRequest(new { message = "Token cannot be null or empty." });
            }

            var token = tokenAuth.Substring("Bearer ".Length).Trim();

            return Ok(new { message = "You have successfully logged out." });
        }


    }
}