using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.Persistence;

namespace SimpleBoards.Core.ReadModels
{
    public class Database : IDatabase
    {
        public BoardsContext Context { get; }

        public Database(BoardsContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Board> Boards => Context.Boards.AsNoTracking();

        public IQueryable<Issue> Issues => Context.Issues.AsNoTracking();

        public IQueryable<Comment> Comments => Context.Comments.AsNoTracking();

        public IQueryable<User> Users => Context.Users.AsNoTracking();
    }
}