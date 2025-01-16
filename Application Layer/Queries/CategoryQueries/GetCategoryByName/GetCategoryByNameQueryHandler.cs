using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.CategoryQueries.GetCategoryByName
{
    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, OperationResult<Category>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public GetCategoryByNameQueryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<Category>> Handle(GetCategoryByNameQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.Name))
            {
                return OperationResult<Category>.Failure("Category name cannot be empty.");
            }

            var categories = await _categoryRepository.FindAsync(c => c.Name == query.Name, cancellationToken);
            var category = categories.FirstOrDefault();

            if (category == null)
            {
                return OperationResult<Category>.Failure($"Category with name {query.Name} not found.");
            }

            return OperationResult<Category>.Successfull(category);
        }
    }
}

