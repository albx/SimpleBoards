using System;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Web.Models.Issues
{
    public class IssueDetailModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Issue.IssueType Type { get; set; }

        public Issue.IssueState State { get; set; }

        public BoardDescriptor Board { get; set; }

        public UserDescriptor Reporter { get; set; }

        public UserDescriptor Assignee { get; set; }

        public UserDescriptor Tester { get; set; }

        public DateTime CreatedAt { get; set; }

        public class BoardDescriptor
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class UserDescriptor
        {
            public string Id { get; set; }

            public string UserName { get; set; }
        }
    }
}