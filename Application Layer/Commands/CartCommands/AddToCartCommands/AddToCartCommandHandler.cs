using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
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

            Cart? cart = null;

            if (!string.IsNullOrEmpty(cartItemDto.UserId))
            {
                cart = await _cartRepository.GetCartByUserIdAsync(cartItemDto.UserId, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(cartItemDto.SessionId.ToString()))

            {
                cart = await _cartRepository.GetCartBySessionIdAsync(cartItemDto.SessionId??0, cancellationToken);
            }

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = cartItemDto.UserId,
                    SessionId = cartItemDto.SessionId
                };
                await _cartRepository.AddCartAsync(cart, cancellationToken);
            }
            else
            {
               
                if (string.IsNullOrEmpty(cart.UserId) && !string.IsNullOrEmpty(cartItemDto.UserId))
                {
                    cart.UserId = cartItemDto.UserId;
                }
            }

            var existingItem = cart.Items.FirstOrDefault(
                item => item.ProductId == cartItemDto.ProductId);

            if (existingItem != null)
            {
                cartItem.Quantity += request.CartItem.Quantity;
                cartItem.SetTotalPrice();
            }
            else
            {
                var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId, cancellationToken);
                if (product == null)
                {
                    throw new Exception("Produkten kunde inte hittas.");
                }
                var newCartItem = _mapper.Map<CartItem>(cartItemDto);
                newCartItem.UnitPrice = product.Price;
                newCartItem.Size = product.Size;
                newCartItem.SetTotalPrice();
                cart.Items.Add(newCartItem);
            }

            await _cartRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Successfull(cart.Id);
        }
    }
}
