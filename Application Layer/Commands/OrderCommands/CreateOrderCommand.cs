using Application_Layer.DTO.OrderDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<OperationResult<Guid>>
    {
        public CheckoutDto Checkout { get; }

        public CreateOrderCommand(CheckoutDto checkout)
        {
            Checkout = checkout;
        }
    }
}
