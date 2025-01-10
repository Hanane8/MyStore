using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ClothingTypeCommands.DeleteClothingTypeCommands
{
    public class DeleteClothingTypeCommand : IRequest<OperationResult<Guid>>
    {
        public Guid Id { get; set; }

        public DeleteClothingTypeCommand(Guid id)
        {
            Id = id;
        }
    }
}
