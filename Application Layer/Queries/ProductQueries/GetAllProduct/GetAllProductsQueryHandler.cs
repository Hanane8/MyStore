using Application_Layer.DTO.ProductsDto;
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

namespace Application_Layer.Queries.ProductQueries.GetAllProduct
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, OperationResult<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            if (products == null || !products.Any())
            {
                return OperationResult<IEnumerable<ProductDTO>>.Failure("No products found", "No products found in the database.");
            }

            var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return OperationResult<IEnumerable<ProductDTO>>.Successfull(productDTO);
        }
    }
}
