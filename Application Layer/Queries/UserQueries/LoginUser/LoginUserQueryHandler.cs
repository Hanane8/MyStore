using Application_Layer.Helpers;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Application_Layer.Interfaces;
using Application_Layer.DTO.UserDto;

namespace Application_Layer.Queries.UserQueries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OperationResult<LoginUserResultDto>>
    {
        private readonly  IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResult<LoginUserResultDto>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.LoginUserDTO?.Email);
            if (user == null)
            {
                return OperationResult<LoginUserResultDto>.Failure("Felaktig e-post.");
            }

            var passwordResult = await _userRepository.VerifyPasswordAsync(user.Email, request.LoginUserDTO.Password);
            if (!passwordResult)
            {
                return OperationResult<LoginUserResultDto>.Failure("Felaktig lösenord.");
            }

            var token = await _userRepository.GenerateJwtTokenAsync(user);
            var result = new LoginUserResultDto
            {
                Token = token,
                UserId = user.Id,
                Message = "Login successful."
            };

            return OperationResult<LoginUserResultDto>.Successfull(result);
        }
    }
    
}