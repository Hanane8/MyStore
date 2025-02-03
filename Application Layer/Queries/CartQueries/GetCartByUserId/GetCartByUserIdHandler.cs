using Application_Layer.DTO.CartDTO;
using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.CartQueries.GetCartByUserId
{
    public class GetCartByUserIdHandler : IRequestHandler<GetCartByUserIdQuery, OperationResult<CartDto>>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public GetCartByUserIdHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<CartDto>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return OperationResult<CartDto>.Failure("UserId is required.");
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId, cancellationToken);

            if (cart == null)
            {
                return OperationResult<CartDto>.Failure("Cart not found.");
            }

            var cartDto = _mapper.Map<CartDto>(cart);
            return OperationResult<CartDto>.Successfull(cartDto);
        }
    }
}
