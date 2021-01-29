using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace SimpleBoards.Identity.Grpc
{
    public class UsersService : Users.UsersBase
    {
        private readonly ILogger<UsersService> _logger;

        public UsersService(ILogger<UsersService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));    
        }
        
        public override Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            return base.RegisterUser(request, context);
        }
    }   
}