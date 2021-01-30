using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SimpleBoards.Core.Identity.Models;

namespace SimpleBoards.Identity.Grpc
{
    public class UsersService : Users.UsersBase
    {
        private readonly ILogger<UsersService> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersService(UserManager<ApplicationUser> userManager, ILogger<UsersService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));    
        }
        
        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("User creation failed with errors {Errors}", result.Errors);

                var errorDescription = string.Join(",", result.Errors);
                throw new RpcException(new Status(StatusCode.Aborted, errorDescription));
            }

            _logger.LogInformation(
                "Users {UserName} ({UserEmail}) created with Id {UserId}", 
                request.UserName,
                request.Email,
                user.Id);

            var response = new RegisterUserResponse { UserId = user.Id };
            return response;
        }
    }   
}