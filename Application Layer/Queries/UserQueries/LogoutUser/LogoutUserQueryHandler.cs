using Application_Layer.Helpers;
using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.UserQueries.LogoutUser
{
    public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;  
        public LogoutUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;  
        }

        public async Task<OperationResult<string>> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByTokenAsync(request.Token);
            if (user == null)
            {
                return OperationResult<string>.Failure("User not found or token is invalid.");
            }

            await _signInManager.SignOutAsync(); 
            
            return OperationResult<string>.Successfull("User logged out successfully.");
        }
    }



}

