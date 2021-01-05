using System.Linq;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.ReadModels
{
    public interface IDatabase
    {
        IQueryable<Board> Boards { get; }

        IQueryable<Issue> Issues { get; }

        IQueryable<Comment> Comments { get; }

        IQueryable<User> Users { get; }
    }
}