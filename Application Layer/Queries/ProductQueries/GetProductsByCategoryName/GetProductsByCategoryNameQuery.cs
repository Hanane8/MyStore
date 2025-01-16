using Application_Layer.DTO.ProductsDto;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ProductQueries.GetProductsByCategoryName
{
    public class GetProductsByCategoryNameQuery : IRequest<OperationResult<IEnumerable<ProductDTO>>>
    {
        public string CategoryName { get; set; }

        public GetProductsByCategoryNameQuery(string categoryName)
        {
            CategoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
        }
    }
}
