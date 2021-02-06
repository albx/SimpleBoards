using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Commands;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Web.Models.Issues;

namespace SimpleBoards.Web.Api.Services
{
    public class IssuesControllerServices
    {
        public IDatabase Database { get; }
        public IIssueCommands Commands { get; }

        public IssuesControllerServices(IDatabase database, IIssueCommands commands)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public IssuesListModel GetIssuesListForBoard(int boardId)
        {
            var issues = Database.Issues
                .Include(i => i.Board)
                .Where(i => i.BoardId == boardId)
                .Where(i => i.State != Issue.IssueState.Closed)
                .Select(i => new IssuesListModel.IssueListItem
                {
                    Id = i.Id,
                    CreatedAt = i.CreatedAt,
                    State = i.State,
                    Title = i.Title,
                    Type = i.Type
                }).ToArray();

            var model = new IssuesListModel
            {
                Issues = issues
            };

            return model;
        }

        public IssueDetailModel GetIssueDetail(int issueId)
        {
            var issue = Database.Issues
                .Include(i => i.Reporter)
                .Include(i => i.Assignee)
                .Include(i => i.Tester)
                .Include(i => i.Board)
                .SingleOrDefault(i => i.Id == issueId);

            if (issue is null)
            {
                return null;
            }

            var model = new IssueDetailModel
            {
                Id = issue.Id,
                Assignee = issue.Assignee is null ? null : new IssueDetailModel.UserDescriptor
                { 
                    Id = issue.Assignee.Id,
                    UserName = issue.Assignee.UserName
                },
                Board = new IssueDetailModel.BoardDescriptor
                {
                    Id = issue.Board.Id,
                    Name = issue.Board.Name
                },
                CreatedAt = issue.CreatedAt,
                Description = issue.Description,
                Reporter = new IssueDetailModel.UserDescriptor
                {
                    Id = issue.Reporter.Id,
                    UserName = issue.Reporter.UserName
                },
                State = issue.State,
                Tester = issue.Tester is null ? null : new IssueDetailModel.UserDescriptor
                {
                    Id = issue.Tester.Id,
                    UserName = issue.Tester.UserName
                },
                Title = issue.Title,
                Type = issue.Type
            };

            return model;
        }

        public async Task<int> OpenNewIssue(NewIssueModel model)
        {
            var newIssueId = await Commands.OpenNewIssue(
                model.BoardId, 
                model.ReporterId, 
                model.Type, 
                model.Title, 
                model.Description);
            
            return newIssueId;
        }

        public async Task AssigneIssue(int issueId, AssignIssueModel model) => await Commands.AssignIssue(issueId, model.AssigneeId);

        public async Task StartWorkingOnIssue(int issueId) => await Commands.StartWorkOnIssue(issueId);

        public async Task CloseIssue(int issueId) => await Commands.CloseIssue(issueId);

        public async Task CompleteIssue(int issueId) => await Commands.CompleteIssue(issueId);

        public async Task RejectIssue(int issueId) => await Commands.RejectIssue(issueId);

        public async Task MoveIssueToTesting(int issueId, MoveIssueToTestingModel model) => await Commands.MoveIssueToTesting(issueId, model.TesterId);
    }
}