using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleBoards.Core.Commands;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Web.Models.Users;

namespace SimpleBoards.Web.Api.Services
{
    public class UsersControllerServices
    {
        public IDatabase Database { get; }

        public IUserCommands Commands { get; }

        public UsersControllerServices(IDatabase database, IUserCommands commands)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public UsersListModel GetUsersList()
        {
            var users = Database.Users
                .Select(u => new UsersListModel.UserListItem
                {
                    Id = u.Id,
                    UserName = u.UserName
                }).ToArray();

            return new UsersListModel
            {
                Users = users
            };
        }

        public UserDetailModel GetUserDetail(string userId)
        {
            var user = Database.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                return null;
            }

            return new UserDetailModel
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public Task<string> RegisterUser(RegisterUserModel model) => Commands.RegisterUser(model.UserName, model.Email, model.Password);
    }
}