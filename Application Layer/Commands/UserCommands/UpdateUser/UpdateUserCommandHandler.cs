using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Layer.Commands.UserCommands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OperationResult<User>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

                if (user == null)
                {
                    return OperationResult<User>.Failure("User not found.");
                }

                _mapper.Map(request.UpdateUserDto, user);

                _userRepository.Update(user);

                await _userRepository.SaveAsync(cancellationToken);

                return OperationResult<User>.Successfull(user, "User updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult<User>.Failure(ex.Message, "An error occurred while updating the user.");
            }
        }

    }
}
