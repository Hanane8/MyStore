using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ClothingTypeQuries.GetClothingTypeByCategory
{
    public class GetClothingTypeByCategoryQuery : IRequest<OperationResult<List<ClothingType>>>
    {
        public Guid CategoryId { get; }

        public GetClothingTypeByCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
