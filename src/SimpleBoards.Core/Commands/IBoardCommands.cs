using System.Threading.Tasks;

namespace SimpleBoards.Core.Commands
{
    public interface IBoardCommands
    {
        Task<int> CreateNewBoard(string boardName);

        Task ChangeBoardName(int boardId, string newBoardName);

        Task DeleteBoard(int boardId);

        Task RestoreBoard(int boardId);
    }
}