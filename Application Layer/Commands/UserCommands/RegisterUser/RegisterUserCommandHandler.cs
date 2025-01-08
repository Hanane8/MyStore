using Application_Layer.Commands.UserCommands.RegisterUser;
using Application_Layer.DTO.UserDto;
using AutoMapper;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using Infrastructure_Layer.Repositories;
using MediatR;

namespace Application_Layer.Commands.UserCommands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new RegisterUserCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return OperationResult<string>.Failure( "Validation failed");
            }

            var addUserDto = _mapper.Map<AddUserDTO>(request.NewUser);
            var user = _mapper.Map<User>(addUserDto);

            await _userRepository.AddAsync(user, cancellationToken);

            return OperationResult<string>.Successfull("User created successfully");
        }
    }
}