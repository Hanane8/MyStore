using Application_Layer.DTO.ProductsDto;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.ProductQueries.GetAllProduct
{
    public class GetAllProductsQuery : IRequest<OperationResult<IEnumerable<ProductDTO>>>
    {
    }
}
