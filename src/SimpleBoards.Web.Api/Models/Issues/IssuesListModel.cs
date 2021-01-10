using System;
using System.Collections.Generic;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Web.Api.Models.Issues
{
    public class IssuesListModel
    {
        public IEnumerable<IssueListItem> Issues { get; set; }

        public class IssueListItem
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public Issue.IssueType Type { get; set; }

            public Issue.IssueState State { get; set; }

            public DateTime CreatedAt { get; set; }
        }
    }
}