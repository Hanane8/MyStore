using Application_Layer.DTO.UserDto;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.UserQueries.LoginUser
{
    public class LoginUserQuery : IRequest<OperationResult<string>>
    {
        public LoginUserDTO LoginUserDTO { get; set; }

        public LoginUserQuery(LoginUserDTO loginUserDTO)
        {
            LoginUserDTO = loginUserDTO;
        }
    }
}
