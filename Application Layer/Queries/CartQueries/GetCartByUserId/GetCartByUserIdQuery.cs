using Application_Layer.DTO.CartDTO;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.CartQueries.GetCartByUserId
{
    public class GetCartByUserIdQuery : IRequest<OperationResult<CartDto>>
    {
        public string UserId { get; }

        public GetCartByUserIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}
