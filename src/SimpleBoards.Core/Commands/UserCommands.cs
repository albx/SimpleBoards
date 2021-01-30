using System;
using System.Threading.Tasks;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.Persistence;

namespace SimpleBoards.Core.Commands
{
    public class UserCommands : IUserCommands
    {
        public BoardsContext Context { get; }
        public Users.UsersClient IdentityClient { get; }

        public UserCommands(BoardsContext context, Users.UsersClient identityClient)
        {
            Context = context ?? throw new System.ArgumentNullException(nameof(context));
            IdentityClient = identityClient ?? throw new ArgumentNullException(nameof(identityClient));
        }

        public async Task<string> RegisterUser(string userName, string email, string password)
        {
            try
            {
                var identityResponse = await IdentityClient.RegisterUserAsync(
                    new RegisterUserRequest
                    {
                        Email = email,
                        Password = password,
                        UserName = userName
                    });

                var user = new User
                {
                    Id = identityResponse.UserId,
                    UserName = userName
                };

                Context.Users.Add(user);
                await Context.SaveChangesAsync();

                return user.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}