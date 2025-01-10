using Application_Layer.DTO.CartDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.CartCommands.AddToCartCommands
{
    public class AddToCartCommand : IRequest<OperationResult<Guid>>
    {
        public AddToCartDTO CartItem { get; set; }

        public AddToCartCommand(AddToCartDTO cartItem)
        {
            CartItem = cartItem;
        }
    }
}
