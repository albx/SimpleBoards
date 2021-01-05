using System;
using System.Collections.Generic;

namespace SimpleBoards.Core.Models
{
    public class Issue
    {
        #region Properties
        public int Id { get; protected set; }

        public string Title { get; protected set; }

        public string Description { get; protected set; }

        public IssueType Type { get; protected set; }

        public IssueState State { get; protected set; }

        public virtual Board Board { get; protected set; }

        public virtual User Reporter { get; protected set; }

        public virtual User Assignee { get; protected set; }

        public virtual User Tester { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; }

        public DateTime CreatedAt { get; protected set; }
        #endregion

        #region Constructor 
        protected Issue() 
        { 
            Comments = new HashSet<Comment>();
        }
        #endregion

        #region Factory method
        public static Issue OpenNewIssue(Board board, User reporter, IssueType type, string title, string description)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            if (reporter is null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            }

            var issue = new Issue
            {
                Board = board,
                Reporter = reporter,
                Type = type,
                Title = title,
                Description = description,
                State = IssueState.New,
                CreatedAt = DateTime.UtcNow
            };

            return issue;
        }
        #endregion

        #region Public methods
        public void Assign(User assignee) => Assignee = assignee ?? throw new ArgumentNullException(nameof(assignee));

        public void SetAsInProgress() => State = IssueState.InProgress;

        public void SetAsClosed() => State = IssueState.Closed;

        public void SetAsDone() => State = IssueState.Done;

        public void Reject() => State = IssueState.Rejected;

        public void MoveToTesting(User tester)
        {
            Tester = tester ?? throw new ArgumentNullException(nameof(tester));
            State = IssueState.Testing;
        }

        public void AddComment(User user, string text)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
            }

            var comment = new Comment
            {
                User = user,
                Text = text
            };

            Comments.Add(comment);
        }
        #endregion

        #region Inner classes
        public enum IssueType
        {
            Feature,
            Bug,
            Refactor
        }

        public enum IssueState
        {
            New,
            InProgress,
            Closed,
            Done,
            Rejected,
            Testing
        }
        #endregion
    }
}