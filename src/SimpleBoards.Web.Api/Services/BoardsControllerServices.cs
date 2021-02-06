using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Commands;
using SimpleBoards.Core.Models;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Web.Models.Boards;

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
                .Include(b => b.Issues)
                .Where(b => !b.Deleted)
                .Select(b => new BoardListModel.BoardListItem
                {
                    Id = b.Id,
                    Name = b.Name,
                    NumberOfIssuesDone = b.Issues.Count(i => i.State == Issue.IssueState.Done),
                    NumberOfNewIssues = b.Issues.Count(i => i.State == Issue.IssueState.New),
                    NumberOfIssuesInProgress = b.Issues.Count(i => i.State == Issue.IssueState.InProgress),
                    NumberOfIssuesToDo = b.Issues.Count(i => i.State == Issue.IssueState.ToDo),
                    NumberOfIssueInTesting = b.Issues.Count(i => i.State == Issue.IssueState.Testing)
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
            var newBoardId = await Commands.CreateNewBoard(model.Name);
            return newBoardId;
        }

        public async Task EditBoard(int boardId, BoardModel model) => await Commands.ChangeBoardName(boardId, model.Name);

        public async Task DeleteBoard(int boardId) => await Commands.DeleteBoard(boardId);

        public async Task RestoreBoard(int boardId) => await Commands.RestoreBoard(boardId);
    }
}