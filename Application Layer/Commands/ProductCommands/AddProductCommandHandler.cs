using Application_Layer.Commands.ProductCommands;
using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Layer.Commands.ProductCommands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, OperationResult<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly AddProductCommandValidator _validator;

        public AddProductCommandHandler(
            IProductRepository productRepository,
            IMapper mapper,
            AddProductCommandValidator validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<OperationResult<Guid>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return OperationResult<Guid>.Failure(
                     string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)),
                     "Failed to add product due to validation errors."
                 );
            }

            try
            {
                var product = _mapper.Map<Product>(request.Product);

                await _productRepository.AddAsync(product, cancellationToken);

                return OperationResult<Guid>.Successfull(product.Id, "Product added successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<Guid>.Failure(ex.Message, "Failed to add product.");
            }
        }
    }
}