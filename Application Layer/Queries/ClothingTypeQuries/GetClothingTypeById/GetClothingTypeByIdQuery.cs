using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeById
{
    public class GetClothingTypeByIdQuery : IRequest<OperationResult<ClothingType>>
    {
        public Guid Id { get; }

        public GetClothingTypeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
