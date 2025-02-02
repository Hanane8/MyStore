using Application_Layer.DTO.UserDto;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.UserCommands.UpdateUser
{
    public class UpdateUserCommand : IRequest<OperationResult<User>>
    {
        public string UserId { get; set; }  
        public UpdateUserDto UpdateUserDto { get; set; }

        public UpdateUserCommand(UpdateUserDto updateUserDto, string userId)
        {
            UpdateUserDto = updateUserDto;
            UserId = userId;
        }
    }


}
