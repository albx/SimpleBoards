using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Commands;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Web.Models.Comments;

namespace SimpleBoards.Web.Api.Services
{
    public class CommentsControllerServices
    {
        public IDatabase Database { get; }
        public IIssueCommands Commands { get; }

        public CommentsControllerServices(IDatabase database, IIssueCommands commands)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public IEnumerable<CommentModel> GetComments(int issueId)
        {
            var comments = Database.Comments
                .Include(c => c.User)
                .Where(c => c.IssueId == issueId)
                .OrderBy(c => c.CommentedAt)
                .Select(c => new CommentModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    CommentedAt = c.CommentedAt,
                    User = new CommentModel.UserDescriptor
                    {
                        Id = c.User.Id,
                        UserName = c.User.UserName
                    }
                }).ToArray();

            return comments;
        }

        public CommentModel GetCommentDetail(int issueId, int commentId)
        {
            var comment = Database.Comments
                .Include(c => c.User)
                .Where(c => c.IssueId == issueId)
                .SingleOrDefault(c => c.Id == commentId);

            if (comment is null)
            {
                return null;
            }

            var model = new CommentModel
            {
                Id = comment.Id,
                Text = comment.Text,
                CommentedAt = comment.CommentedAt,
                User = new CommentModel.UserDescriptor
                {
                    Id = comment.User.Id,
                    UserName = comment.User.UserName
                }
            };

            return model;
        }

        public async Task AddNewComment(int issueId, NewCommentModel model) => await Commands.AddCommentToIssue(issueId, model.UserId, model.CommentText);

        public async Task DeleteComment(int issueId, int commentId) => await Commands.RemoveCommentFromIssue(issueId, commentId);
    }
}