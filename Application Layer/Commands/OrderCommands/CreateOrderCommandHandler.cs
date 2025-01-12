using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.OrderCommands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OperationResult<Guid>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IGenericRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Checkout.CartItems == null || !request.Checkout.CartItems.Any())
                {
                    return OperationResult<Guid>.Failure("CartItems cannot be empty.");
                }

                var order = _mapper.Map<Order>(request.Checkout);

                await _orderRepository.AddAsync(order, cancellationToken);

                return OperationResult<Guid>.Successfull(order.Id, "Order created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Guid>.Failure(ex.Message, "Order creation failed.");
            }
        }
    }
}
