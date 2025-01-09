using Application_Layer.DTO.ProductsDto;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ProductCommands.AddProductCommands
{
    public class AddProductCommand : IRequest<OperationResult<Guid>>
    {
        public AddProductDTO Product { get; set; }

        public AddProductCommand(AddProductDTO product)
        {
            Product = product;
        }
    }
}
