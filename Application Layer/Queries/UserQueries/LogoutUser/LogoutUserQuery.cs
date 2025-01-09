using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.UserQueries.LogoutUser
{
    public class LogoutUserQuery : IRequest<OperationResult<string>>
    {
        public string? Token { get; set; }
    }
}
