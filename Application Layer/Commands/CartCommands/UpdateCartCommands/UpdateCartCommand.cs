using Application_Layer.DTO.CartItemDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.CartCommands.UpdateCartCommands
{
    public class UpdateCartCommand : IRequest<OperationResult<bool>>
    {
        public UpdateCartItemDTO CartItem { get; set; }

        public UpdateCartCommand(UpdateCartItemDTO cartItem)
        {
            CartItem = cartItem;
        }
    }
}
