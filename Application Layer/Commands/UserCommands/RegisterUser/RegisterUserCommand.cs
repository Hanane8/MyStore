using Application_Layer.DTO.UserDto;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommand: IRequest<OperationResult<AddUserDTO>>
    {
        public AddUserDTO newUser { get; set; }
        public RegisterUserCommand(AddUserDTO userToAdd)
        {
            newUser = userToAdd;
        }
    }
}
