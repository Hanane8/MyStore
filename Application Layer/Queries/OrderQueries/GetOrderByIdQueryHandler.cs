using Application_Layer.DTO;
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

namespace Application_Layer.Queries.OrderQueries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OperationResult<OrderDto>>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IGenericRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

                if (order == null)
                {
                    return OperationResult<OrderDto>.Failure("Order not found.");
                }

                var orderDto = _mapper.Map<OrderDto>(order);
                return OperationResult<OrderDto>.Successfull(orderDto, "Order retrieved successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<OrderDto>.Failure(ex.Message, "Error retrieving the order.");
            }
        }
    }
}
