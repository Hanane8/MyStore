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
        private readonly TokenHelper _tokenHelper;
        public LogoutUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _signInManager = signInManager;  
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResult<string>> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return OperationResult<string>.Failure("Token is required for logout.");
            }

            Console.WriteLine($"Handling logout for token: {request.Token}");

            var claimsPrincipal = _tokenHelper.ValidateJwtToken(request.Token);
            if (claimsPrincipal == null)
            {
                Console.WriteLine("Invalid or expired token.");
                return OperationResult<string>.Failure("Token is invalid or expired.");
            }

            var user = await _userRepository.GetUserByTokenAsync(request.Token);
            if (user == null)
            {
                Console.WriteLine($"Logout failed: No user associated with token '{request.Token}'.");
                return OperationResult<string>.Failure("User not found or token is invalid.");
            }

            await _signInManager.SignOutAsync();

            Console.WriteLine($"User {user.Email} logged out successfully.");
            return OperationResult<string>.Successfull("User logged out successfully.");
        }

    }



}

