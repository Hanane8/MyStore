using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Domain_Layer.OperationResultCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Queries.UserQueries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IGenericRepository<User> _userRepository;

        public GetUserByIdHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId.ToString(), cancellationToken);

            if (user == null)
            {
                return OperationResult<User>.Failure("User not found.");
            }

            return OperationResult<User>.Successfull(user);
        }
    }
}