using System;
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

        public async Task CreateNewBoard(BoardModel model)
        {
            var response = await Http.PostAsJsonAsync("api/boards", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"An error occured. Status code {response.StatusCode}, Content: {await response.Content.ReadAsStringAsync()}");
            }
        }

        public Task<BoardModel> GetBoardDetail(int boardId) => Http.GetFromJsonAsync<BoardModel>($"api/boards/{boardId}");
    }
}