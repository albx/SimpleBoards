using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleBoards.Web.Models.Boards;

namespace SimpleBoards.Web.App.Http
{
    public class BoardsHttpClient
    {
        public HttpClient Http { get; }

        public BoardsHttpClient(HttpClient http)
        {
            Http = http;
        }

        public Task<BoardListModel> GetBoardsList() => Http.GetFromJsonAsync<BoardListModel>("api/boards");

        public Task CreateNewBoard(BoardModel model) => Http.PostAsJsonAsync("api/boards", model);

        public Task<BoardModel> GetBoardDetail(int boardId) => Http.GetFromJsonAsync<BoardModel>($"api/boards/{boardId}");
    }
}