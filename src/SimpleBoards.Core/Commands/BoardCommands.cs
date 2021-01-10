using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.Persistence;

namespace SimpleBoards.Core.Commands
{
    public class BoardCommands : IBoardCommands
    {
        public BoardsContext Context { get; }

        public BoardCommands(BoardsContext context)
        {
            Context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public async Task ChangeBoardName(int boardId, string newBoardName)
        {
            var board = Context.Boards.SingleOrDefault(b => b.Id == boardId);
            if (board is null)
            {
                throw new ArgumentOutOfRangeException(nameof(boardId));
            }

            board.ChangeName(newBoardName);
            await Context.SaveChangesAsync();
        }

        public async Task<int> CreateNewBoard(string boardName)
        {
            var board = Board.NewBoard(boardName);
            Context.Add(board);

            await Context.SaveChangesAsync();

            return board.Id;
        }

        public async Task DeleteBoard(int boardId)
        {
            var board = Context.Boards.SingleOrDefault(b => b.Id == boardId);
            if (board is null)
            {
                throw new ArgumentOutOfRangeException(nameof(boardId));
            }

            board.Delete();
            await Context.SaveChangesAsync();
        }

        public async Task RestoreBoard(int boardId)
        {
            var board = Context.Boards.SingleOrDefault(b => b.Id == boardId);
            if (board is null)
            {
                throw new ArgumentOutOfRangeException(nameof(boardId));
            }

            board.Restore();
            await Context.SaveChangesAsync();
        }
    }
}