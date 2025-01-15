using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.CategoryQueries.GetAllCategories
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, OperationResult<IEnumerable<Category>>>
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public GetAllCategoryQueryHandler(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<IEnumerable<Category>>> Handle(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync(cancellationToken);
            return OperationResult<IEnumerable<Category>>.Successfull(categories);
        }
    }
}