using Application_Layer.DTO.ProductsDto;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ProductQueries.GetProductsByClothingType
{
    public class GetProductsByClothingTypeQuery : IRequest<OperationResult<IEnumerable<ProductDTO>>>
    {
        public Guid ClothingTypeId { get; set; }

        public GetProductsByClothingTypeQuery(Guid clothingTypeId)
        {
            ClothingTypeId = clothingTypeId;
        }
    }
}
