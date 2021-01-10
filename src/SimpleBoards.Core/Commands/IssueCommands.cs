using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.Persistence;

namespace SimpleBoards.Core.Commands
{
    public class IssueCommands : IIssueCommands
    {
        public BoardsContext Context { get; }

        public IssueCommands(BoardsContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddCommentToIssue(int issueId, string userId, string commentText)
        {
            var issue = Context.Issues
                .Include(i => i.Comments)
                .SingleOrDefault(i => i.Id == issueId);

            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            var user = Context.Users.SingleOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            issue.AddComment(user, commentText);
            await Context.SaveChangesAsync();
        }

        public async Task AssignIssue(int issueId, string assigneeId)
        {
            var issue = Context.Issues
                .Include(i => i.Assignee)
                .SingleOrDefault(i => i.Id == issueId);

            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            var assignee = Context.Users.SingleOrDefault(u => u.Id == assigneeId);
            if (assignee is null)
            {
                throw new ArgumentOutOfRangeException(nameof(assigneeId));
            }
            
            issue.Assign(assignee);
            await Context.SaveChangesAsync();
        }

        public async Task MoveIssueToTesting(int issueId, string testerId)
        {
            var issue = Context.Issues
                .Include(i => i.Tester)
                .SingleOrDefault(i => i.Id == issueId);

            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            var tester = Context.Users.SingleOrDefault(u => u.Id == testerId);
            if (tester is null)
            {
                throw new ArgumentOutOfRangeException(nameof(testerId));
            }

            issue.MoveToTesting(tester);
            await Context.SaveChangesAsync();
        }

        public async Task OpenNewIssue(int boardId, string reporterId, Issue.IssueType type, string title, string description)
        {
            var board = Context.Boards.SingleOrDefault(b => b.Id == boardId);
            if (board is null)
            {
                throw new ArgumentOutOfRangeException(nameof(boardId));
            }

            var reporter = Context.Users.SingleOrDefault(u => u.Id == reporterId);
            if (reporter is null)
            {
                throw new ArgumentOutOfRangeException(nameof(reporterId));
            }

            var issue = Issue.OpenNewIssue(board, reporter, type, title, description);
            Context.Add(issue);

            await Context.SaveChangesAsync();
        }

        public async Task RejectIssue(int issueId)
        {
            var issue = Context.Issues.SingleOrDefault(i => i.Id == issueId);
            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            issue.Reject();
            await Context.SaveChangesAsync();
        }

        public async Task RemoveCommentFromIssue(int issueId, int commentId)
        {
            var issue = Context.Issues
                .Include(i => i.Comments)
                .SingleOrDefault(i => i.Id == issueId);

            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            issue.RemoveComment(commentId);
            await Context.SaveChangesAsync();
        }

        public async Task CloseIssue(int issueId)
        {
            var issue = Context.Issues.SingleOrDefault(i => i.Id == issueId);
            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            issue.Close();
            await Context.SaveChangesAsync();
        }

        public async Task MarkIssueAsDone(int issueId)
        {
            var issue = Context.Issues.SingleOrDefault(i => i.Id == issueId);
            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            issue.MarkAsDone();
            await Context.SaveChangesAsync();
        }

        public async Task StartWorkOnIssue(int issueId)
        {
            var issue = Context.Issues.SingleOrDefault(i => i.Id == issueId);
            if (issue is null)
            {
                throw new ArgumentOutOfRangeException(nameof(issueId));
            }

            issue.Start();
            await Context.SaveChangesAsync();
        }
    }
}