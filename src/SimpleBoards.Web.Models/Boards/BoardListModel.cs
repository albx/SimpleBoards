using System.Collections.Generic;

namespace SimpleBoards.Web.Models.Boards
{
    public class BoardListModel
    {
        public IEnumerable<BoardListItem> Boards { get; set; }

        public class BoardListItem
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int TotalNumberOfIssues { get; set; }

            public int NumberOfNewIssues { get; set; }

            public int NumberOfIssuesToDo { get; set; }

            public int NumberOfIssuesInProgress { get; set; }

            public int NumberOfIssuesDone { get; set; }

            public int NumberOfIssueInTesting { get; set; }
        }
    }
}