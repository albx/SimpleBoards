using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleBoards.Web.Models.Issues;

namespace SimpleBoards.Web.App.Http
{
    public class IssuesHttpClient
    {
        public HttpClient Http { get; }

        public IssuesHttpClient(HttpClient http)
        {
            Http = http;
        }

        public Task<IssuesListModel> GetIssuesList(int boardId) => Http.GetFromJsonAsync<IssuesListModel>($"api/issues?boardId={boardId}");

        public Task OpenNewIssue(NewIssueModel model) => Http.PostAsJsonAsync("api/issues", model);
    }
}