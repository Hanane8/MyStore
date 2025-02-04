using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;

namespace Application_Layer.Commands.CartItemCommands.UpdateCartItemCommands
{
    public class UpdateCartItemQuantityHandler : IRequestHandler<UpdateCartItemQuantityCommand, OperationResult<bool>>
    {
        private readonly ICartRepository _cartRepository;

        public UpdateCartItemQuantityHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<OperationResult<bool>> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId, cancellationToken);
            if (cart == null)
                return OperationResult<bool>.Failure("Cart not found", "Failed to update quantity.");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
            if (item == null)
                return OperationResult<bool>.Failure("Product not found in cart", "Failed to update quantity.");

            item.Quantity = request.Quantity;

            item.SetTotalPrice();

            try
            {
                _cartRepository.Update(cart);
                await _cartRepository.SaveChangesAsync(cancellationToken);
                return OperationResult<bool>.Successfull(true, "Quantity and total price updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Failure(ex.Message, "Failed to update quantity and total price due to an error.");
            }
        }
    }


}