using System;
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

        public async Task AssignIssue(int issueId, AssignIssueModel model)
        {
            var response = await Http.PatchAsync($"api/issues/{issueId}/assign", JsonContent.Create(model));
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Could not assign issue");
            }
        }

        public Task CloseIssue(int issueId) => Http.DeleteAsync($"api/issues/{issueId}");

        public async Task StartIssue(int issueId)
        {
            var response = await Http.PatchAsync($"api/issues/{issueId}/start", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Could not start issue");
            }
        }

        public async Task MoveIssueToTesting(int issueId, MoveIssueToTestingModel model)
        {
            var response = await Http.PatchAsync($"api/issues/{issueId}/testing", JsonContent.Create(model));
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Could not assign issue");
            }
        }
    }
}