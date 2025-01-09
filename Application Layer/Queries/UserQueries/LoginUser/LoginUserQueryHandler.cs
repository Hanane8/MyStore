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

namespace Application_Layer.Queries.UserQueries.LoginUser
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, OperationResult<string>>
    {
        private readonly  IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LoginUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResult<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.LoginUserDTO?.Email);
            if (user == null)
            {
                return OperationResult<string>.Failure("Felaktig e-post.");
            }

            var passwordResult = await _userRepository.VerifyPasswordAsync(user.Email, request.LoginUserDTO.Password);
            if (!passwordResult)
            {
                return OperationResult<string>.Failure("Felaktig lösenord.");
            }

            var token = await _userRepository.GenerateJwtTokenAsync(user, _tokenHelper);
            return OperationResult<string>.Successfull(token);
        }
    }
}