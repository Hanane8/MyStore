using Application_Layer.DTO.ProductsDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ProductQueries.GetProductByCategory
{
    public class GetProductsByCategoryQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public Guid CategoryId { get; set; }

        public GetProductsByCategoryQuery(Guid categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
