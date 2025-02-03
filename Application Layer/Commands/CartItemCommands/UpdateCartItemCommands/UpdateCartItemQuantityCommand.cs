using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.CartItemCommands.UpdateCartItemCommands
{
    public class UpdateCartItemQuantityCommand : IRequest<OperationResult<bool>>
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public UpdateCartItemQuantityCommand(string userId, Guid productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
    }

}
