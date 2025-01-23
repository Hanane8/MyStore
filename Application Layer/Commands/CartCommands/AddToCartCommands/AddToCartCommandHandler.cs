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

            // Kontrollera om produkten finns
            var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId, cancellationToken);
            if (product == null)
            {
                return OperationResult<Guid>.Failure("Produkten finns inte.");
            }

            // Kontrollera lagerstatus
            if (product.Stock < cartItemDto.Quantity)
            {
                return OperationResult<Guid>.Failure("Produkten finns inte tillräckligt i lager.");
            }

            // Kontrollera om användar-ID är giltigt
            if (string.IsNullOrEmpty(cartItemDto.UserId))
            {
                return OperationResult<Guid>.Failure("Ogiltigt användar-ID.");
            }

            // Hämta eller skapa varukorg
            var cart = await _cartRepository.GetCartByUserIdAsync(cartItemDto.UserId, cancellationToken);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = cartItemDto.UserId,
                    Items = new List<CartItem>()
                };
                await _cartRepository.AddCartAsync(cart, cancellationToken);
            }

            // Lägg till eller uppdatera artikel i varukorgen
            var existingItem = cart.Items.FirstOrDefault(item => item.ProductId == cartItemDto.ProductId);
            if (existingItem != null)
            {
                cartItem.Quantity += request.CartItem.Quantity;
                cartItem.SetTotalPrice();
            }
            else
            {
                var newCartItem = _mapper.Map<CartItem>(cartItemDto);
                newCartItem.UnitPrice = product.Price;
                newCartItem.Size = product.Size;
                newCartItem.SetTotalPrice();
                cart.Items.Add(newCartItem);
            }

            // Uppdatera lagret
            product.Stock -= cartItemDto.Quantity;

            // Spara ändringar
            //await _productRepository.UpdateAsync(product, cancellationToken);
            await _cartRepository.SaveChangesAsync(cancellationToken);

            return OperationResult<Guid>.Successfull(cart.Id);
        }
    }
}
