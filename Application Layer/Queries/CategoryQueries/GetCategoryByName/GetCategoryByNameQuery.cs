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
    public class GetCategoryByNameQuery : IRequest<OperationResult<Category>>
    {
        public string Name { get; }

        public GetCategoryByNameQuery(string name)
        {
            Name = name;
        }
    }
}
