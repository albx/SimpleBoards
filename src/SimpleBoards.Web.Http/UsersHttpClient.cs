using SimpleBoards.Web.Models.Users;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SimpleBoards.Web.Http
{
    public class UsersHttpClient
    {
        public HttpClient Http { get; }

        public UsersHttpClient(HttpClient http)
        {
            Http = http;
        }

        public Task<UsersListModel> GetUsers() => Http.GetFromJsonAsync<UsersListModel>("api/users");

        public Task RegisterUser(RegisterUserModel model) => Http.PostAsJsonAsync("api/users", model);
    }
}
