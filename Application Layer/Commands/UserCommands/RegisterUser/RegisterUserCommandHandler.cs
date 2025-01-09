using Application_Layer.Commands.UserCommands.RegisterUser;
using Application_Layer.DTO.UserDto;
using Application_Layer.Interfaces;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;

namespace Application_Layer.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var addUserDto = _mapper.Map<AddUserDTO>(request.NewUser);
            var user = _mapper.Map<User>(addUserDto);

            var result = await _userRepository.CreateUserAsync(user, request.NewUser.Password);
            if (result.Succeeded)
            {
                return OperationResult<string>.Successfull("User created successfully");
            }
            else
            {
                return OperationResult<string>.Failure($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        
    }
}