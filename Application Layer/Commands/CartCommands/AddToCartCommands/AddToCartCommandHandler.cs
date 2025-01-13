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

namespace Application_Layer.Commands.CartCommands.AddToCartCommands
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, OperationResult<Guid>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public AddToCartCommandHandler(ICartRepository cartRepository, IMapper mapper, IGenericRepository<Product> productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<OperationResult<Guid>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartItemDto = request.CartItem;

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

            var existingItem = cart.Items.FirstOrDefault(
                item => item.ProductId == cartItemDto.ProductId && item.Size == cartItemDto.Size);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDto.Quantity;
                existingItem.SetTotalPrice();
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
                newCartItem.SetTotalPrice();
                cart.Items.Add(newCartItem);
            }

            await _cartRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Successfull(cart.Id);
        }
    }
}
