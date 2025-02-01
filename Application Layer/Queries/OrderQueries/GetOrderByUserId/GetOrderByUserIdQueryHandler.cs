using Application_Layer.DTO;
using Application_Layer.Interfaces;
using Application_Layer.Queries.OrderQueries.GetOrderById;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.OrderQueries.GetOrderByUserId
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, OperationResult<IEnumerable<Order>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrdersByUserIdQueryHandler> _logger;

        public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, ILogger<GetOrdersByUserIdQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<OperationResult<IEnumerable<Order>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Fetching orders for user: {request.UserId}");

                var orders = await _orderRepository.GetOrdersByUserIdAsync(request.UserId, cancellationToken);

                if (!orders.Any())
                {
                    _logger.LogWarning($"No orders found for user: {request.UserId}");
                    return OperationResult<IEnumerable<Order>>.Failure($"No orders found for user: {request.UserId}");
                }

                return OperationResult<IEnumerable<Order>>.Successfull(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching orders for user: {request.UserId}");
                return OperationResult<IEnumerable<Order>>.Failure($"Error fetching orders: {ex.Message}");
            }
        }
    }
}