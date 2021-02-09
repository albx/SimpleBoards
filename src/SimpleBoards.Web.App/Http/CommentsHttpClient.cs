using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleBoards.Web.Models.Comments;

namespace SimpleBoards.Web.App.Http
{
    public class CommentsHttpClient
    {
        public HttpClient Http { get; }
        
        public CommentsHttpClient(HttpClient http)
        {
            Http = http;
        }

        public Task<IEnumerable<CommentModel>> GetComments(int issueId) => Http.GetFromJsonAsync<IEnumerable<CommentModel>>($"api/issues/{issueId}/comments");

        public Task AddNewComment(int issueId, NewCommentModel model) => Http.PostAsJsonAsync($"api/issues/{issueId}/comments", model);
    }
}