﻿using Application_Layer.Interfaces;
using AutoMapper;
using MediatR;

namespace Application_Layer.Commands.ProductCommands.UpdateProductCommands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
            {
                return false; 
            }
            _mapper.Map(request.ProductDto, product);

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
