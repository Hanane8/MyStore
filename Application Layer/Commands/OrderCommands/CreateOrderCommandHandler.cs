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
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IGenericRepository<Order> orderRepository, IMapper mapper, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<OperationResult<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkout = request.Checkout;
                var cart = await _cartRepository.GetCartByUserIdAsync(checkout.UserId, cancellationToken);

                if (cart == null || cart.Items.Count == 0)
                {
                    return OperationResult<Guid>.Failure("CartItems cannot be empty.");
                }

                var newOrder = new Order
                {
                    UserId = checkout.UserId,
                    OrderDate = DateTime.UtcNow,
                    OrderStatus = Order.Status.Pending,
                    OrderItems = _mapper.Map<List<OrderItem>>(checkout.CartItems)
                };
                
                foreach (var orderItem in newOrder.OrderItems)
                {
                    var cartItem = cart.Items.FirstOrDefault(ci => ci.ProductId == orderItem.ProductId && ci.Size == orderItem.Size);
                    if (cartItem != null)
                    {
                        orderItem.Quantity = cartItem.Quantity;
                        orderItem.UnitPrice = cartItem.UnitPrice;
                        orderItem.SetTotalPrice();
                    }
                }

                newOrder.SetTotalAmount();


                await _orderRepository.AddAsync(newOrder, cancellationToken);
                cart.Items.Clear();
                await _cartRepository.SaveChangesAsync(cancellationToken);

                return OperationResult<Guid>.Successfull(newOrder.Id, "Order created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Guid>.Failure(ex.Message, "Order creation failed.");
            }
        }
    }
}
