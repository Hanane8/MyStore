using Application_Layer.Commands.ClothingTypeCommands.AddClothingTypeCommand;
using Application_Layer.Commands.ClothingTypeCommands.ClothingTypeCommands;
using Application_Layer.Commands.ClothingTypeCommands.DeleteClothingTypeCommands;
using Application_Layer.DTO.ClothingTypeDTO;
using Application_Layer.Queries.ClothingTypeQuries.GetAllClothingType;
using Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeByCategory;
using Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeById;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClothingTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> AddClothingType([FromBody] AddClothingTypeDTO clothingTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddClothingTypeCommand(clothingTypeDto);

            var result = await _mediator.Send(command);

            if (result.IsSuccessfull)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut("update")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateClothingType([FromBody] UpdateClothingTypeDTO clothingTypeDto)
        {
            var command = new UpdateClothingTypeCommand(clothingTypeDto);

            var result = await _mediator.Send(command);

            if (result.IsSuccessfull)
            {
                return Ok(result.Message); 
            }

            return BadRequest(result.ErrorMessage);
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeleteClothingType(Guid id)
        {
            var command = new DeleteClothingTypeCommand(id);

            var result = await _mediator.Send(command);

            if (result.IsSuccessfull)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<IActionResult> GetClothingTypesByCategory(Guid categoryId)
        {
            var query = new GetClothingTypeByCategoryQuery(categoryId);

            var result = await _mediator.Send(query);

            if (!result.IsSuccessfull)
            {
                return NotFound(result.ErrorMessage); 
            }

            return Ok(result.Data); 
        }

        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAllClothingTypes()
        {
            var query = new GetAllClothingTypeQuery();
            var result = await _mediator.Send(query);

            if (!result.IsSuccessfull)
            {
                return NotFound(result.ErrorMessage); 
            }

            return Ok(result.Data); 
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetClothingTypeById(Guid id)
        {
            var query = new GetClothingTypeByIdQuery(id);
            var result = await _mediator.Send(query);

            if (!result.IsSuccessfull)
            {
                return NotFound(result.ErrorMessage); 
            }

            return Ok(result.Data); 
        }
    }
}



