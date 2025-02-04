using Application_Layer.DTO;
using Application_Layer.DTO.OrderDTO;
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
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, OperationResult<IEnumerable<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrdersByUserIdQueryHandler> _logger;

        public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, ILogger<GetOrdersByUserIdQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

            public async Task<OperationResult<IEnumerable<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"Fetching orders for user: {request.UserId}");

                    var orders = await _orderRepository.GetOrdersByUserIdAsync(request.UserId, cancellationToken);

                    if (!orders.Any())
                    {
                        _logger.LogWarning($"No orders found for user: {request.UserId}");
                        return OperationResult<IEnumerable<OrderDto>>.Failure($"No orders found for user: {request.UserId}");
                    }

                    var orderDtos = orders.Select(order => new OrderDto
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        OrderDate = order.OrderDate,
                        Status = order.OrderStatus.ToString(), 
                        Items = order.OrderItems.Select(item => new OrderItemDTO
                        {
                            Id = item.Id,
                            OrderId = item.OrderId,
                            ProductId = item.ProductId,
                            ProductName = item.Product?.Name,
                            ImageUrl = item.Product?.ImageUrl,
                            Size = item.Size,
                            Quantity = item.Quantity,
                            Price = item.UnitPrice
                        }).ToList()
                    });

                    return OperationResult<IEnumerable<OrderDto>>.Successfull(orderDtos);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error fetching orders for user: {request.UserId}");
                    return OperationResult<IEnumerable<OrderDto>>.Failure($"Error fetching orders: {ex.Message}");
                }
            }
        


    }
}