using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SimpleBoards.Web.Models.Users;

namespace SimpleBoards.Web.App.Http
{
    public class UsersHttpClient
    {
        public HttpClient Http { get; }

        public UsersHttpClient(HttpClient http)
        {
            Http = http;
        }

        public Task<UsersListModel> GetUsers() => Http.GetFromJsonAsync<UsersListModel>("api/users");
    }
}