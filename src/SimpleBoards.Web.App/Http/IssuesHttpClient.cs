using System.Net.Http;

namespace SimpleBoards.Web.App.Http
{
    public class IssuesHttpClient
    {
        public HttpClient Http { get; }

        public IssuesHttpClient(HttpClient http)
        {
            Http = http;
        }
    }
}