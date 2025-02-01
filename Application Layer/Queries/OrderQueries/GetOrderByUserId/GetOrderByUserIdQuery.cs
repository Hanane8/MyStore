using Application_Layer.DTO;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.OrderQueries.GetOrderByUserId
{
    public class GetOrdersByUserIdQuery : IRequest<OperationResult<IEnumerable<Order>>>
    {
        public Guid UserId { get; }

        public GetOrdersByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
