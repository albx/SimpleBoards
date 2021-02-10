using System;

namespace SimpleBoards.Web.Models.Comments
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CommentedAt { get; set; }

        public UserDescriptor User { get; set; }

        public class UserDescriptor
        {
            public string Id { get; set; }

            public string UserName { get; set; }
        }
    }
}