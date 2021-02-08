using System;
using System.Collections.Generic;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Web.Models.Issues
{
    public class IssuesListModel
    {
        public IEnumerable<IssueListItem> Issues { get; set; }

        public class IssueListItem
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Type { get; set; }

            public string State { get; set; }

            public DateTime CreatedAt { get; set; }
        }
    }
}