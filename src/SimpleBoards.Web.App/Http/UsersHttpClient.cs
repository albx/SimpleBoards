using System.Net.Http;

namespace SimpleBoards.Web.App.Http
{
    public class UsersHttpClient
    {
        public HttpClient Http { get; }

        public UsersHttpClient(HttpClient http)
        {
            Http = http;
        }
    }
}