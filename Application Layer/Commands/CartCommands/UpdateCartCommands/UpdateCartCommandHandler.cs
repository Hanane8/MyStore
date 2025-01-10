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

namespace Application_Layer.Commands.CartCommands.UpdateCartCommands
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly IMapper _mapper;

        public UpdateCartCommandHandler(IGenericRepository<CartItem> cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<bool>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var updateDto = request.CartItem;

            var cartItem = await _cartItemRepository.GetByIdAsync(updateDto.CartItemId, cancellationToken);
            if (cartItem == null)
            {
                return OperationResult<bool>.Failure("CartItem not found.");
            }

            _mapper.Map(updateDto, cartItem); 

            _cartItemRepository.Update(cartItem);
            await _cartItemRepository.SaveAsync(cancellationToken);

            return OperationResult<bool>.Successfull(true);
        }
    }
}
