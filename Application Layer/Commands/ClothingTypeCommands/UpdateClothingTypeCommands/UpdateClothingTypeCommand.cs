using Application_Layer.DTO.ClothingTypeDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ClothingTypeCommands.ClothingTypeCommands
{
    public class UpdateClothingTypeCommand : IRequest<OperationResult<Guid>>
    {
        public UpdateClothingTypeDTO ClothingTypeDto { get; set; }

        public UpdateClothingTypeCommand(UpdateClothingTypeDTO clothingTypeDto)
        {
            ClothingTypeDto = clothingTypeDto;
        }
    }
}
