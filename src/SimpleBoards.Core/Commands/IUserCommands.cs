using System.Threading.Tasks;

namespace SimpleBoards.Core.Commands
{
    public interface IUserCommands
    {
        Task<string> RegisterUser(string userName, string email, string password);
    }
}