using Application_Layer.DTO.ClothingTypeDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ClothingTypeCommands.AddClothingTypeCommand
{
    public class AddClothingTypeCommand : IRequest<OperationResult<Guid>>
    {
        public AddClothingTypeDTO ClothingTypeDto { get; set; }

        public AddClothingTypeCommand(AddClothingTypeDTO clothingTypeDto)
        {
            ClothingTypeDto = clothingTypeDto;
        }
    }
}
