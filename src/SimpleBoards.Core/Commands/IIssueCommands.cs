using System.Threading.Tasks;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.Commands
{
    public interface IIssueCommands
    {
        Task OpenNewIssue(int boardId, string reporterId, Issue.IssueType type, string title, string description);

        Task AssignIssue(int issueId, string assigneeId);

        Task StartWorkOnIssue(int issueId);

        Task CloseIssue(int issueId);

        Task MarkIssueAsDone(int issueId);

        Task RejectIssue(int issueId);

        Task MoveIssueToTesting(int issueId, string testerId);

        Task AddCommentToIssue(int issueId, string userId, string commentText);

        Task RemoveCommentFromIssue(int issueId, int commentId);
    }
}