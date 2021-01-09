using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleBoards.Core.Commands;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Web.Api.Models.Boards;

namespace SimpleBoards.Web.Api.Services
{
    public class BoardsControllerServices
    {
        public IDatabase Database { get; }
        public IBoardCommands Commands { get; }

        public BoardsControllerServices(IDatabase database, IBoardCommands commands)
        {
            Database = database ?? throw new ArgumentNullException(nameof(database));
            Commands = commands ?? throw new ArgumentNullException(nameof(commands));
        }

        public BoardListModel GetBoardsList()
        {
            var boards = Database.Boards
                .Where(b => !b.Deleted)
                .Select(b => new BoardListModel.BoardListItem
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToArray();
                
            var model = new BoardListModel
            {
                Boards = boards
            };

            return model;
        }

        public BoardModel GetBoardDetail(int boardId)
        {
            var board = Database.Boards
                .Where(b => !b.Deleted)
                .SingleOrDefault(b => b.Id == boardId);
            
            if (board is null)
            {
                return null;
            }

            return new BoardModel
            {
                Name = board.Name
            };
        }

        public async Task<int> CreateNewBoard(BoardModel model)
        {
            await Commands.CreateNewBoard(model.Name);
            
            var newBoardId = Database.Boards
                .Where(b => !b.Deleted)
                .SingleOrDefault(b => b.Name == model.Name).Id;
            
            return newBoardId;
        }

        public async Task EditBoard(int boardId, BoardModel model) => await Commands.ChangeBoardName(boardId, model.Name);

        public async Task DeleteBoard(int boardId) => await Commands.DeleteBoard(boardId);

        public async Task RestoreBoard(int boardId) => await Commands.RestoreBoard(boardId);
    }
}