using System.Collections.Generic;

namespace SimpleBoards.Web.Api.Models.Users
{
    public class UsersListModel
    {
        public IEnumerable<UserListItem> Users { get; set; }

        public class UserListItem
        {
            public string Id { get; set; }

            public string UserName { get; set; }
        }
    }
}