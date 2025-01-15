using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.CategoryQueries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, OperationResult<Category>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public GetCategoryByIdQueryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<Category>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            if (query.CategoryId == Guid.Empty)
            {
                return OperationResult<Category>.Failure("Category ID cannot be empty.");
            }

            var category = await _categoryRepository.GetByIdAsync(query.CategoryId, cancellationToken);
            if (category == null)
            {
                return OperationResult<Category>.Failure($"Category with ID {query.CategoryId} not found.");
            }

            return OperationResult<Category>.Successfull(category);
        }
    }
}
