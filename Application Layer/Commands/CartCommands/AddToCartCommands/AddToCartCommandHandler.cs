using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Layer.Commands.CartCommands.AddToCartCommands
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, OperationResult<Guid>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddToCartCommandHandler(
            ICartRepository cartRepository,
            IMapper mapper,
            IGenericRepository<Product> productRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult<Guid>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var userId = request.CartItem.UserId;

            if (string.IsNullOrEmpty(userId))
            {
                userId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            }

            if (string.IsNullOrEmpty(userId))
            {
                return OperationResult<Guid>.Failure("User is not authenticated.");
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(userId, cancellationToken)
                        ?? new Cart { UserId = userId };

            var product = await _productRepository.GetByIdAsync(request.CartItem.ProductId, cancellationToken);

            if (product == null)
            {
                return OperationResult<Guid>.Failure("Product not found.");
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == product.Id);

            if (cartItem != null)
            {
                cartItem.Quantity += request.CartItem.Quantity;
                cartItem.SetTotalPrice();
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    Quantity = request.CartItem.Quantity,
                    UnitPrice = product.Price,
                    CartId = cart.Id
                });
            }

            await _cartRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Successfull(cart.Id);
        }
    }
}
