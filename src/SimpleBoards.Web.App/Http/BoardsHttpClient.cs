using System.Net.Http;

namespace SimpleBoards.Web.App.Http
{
    public class BoardsHttpClient
    {
        public HttpClient Http { get; }

        public BoardsHttpClient(HttpClient http)
        {
            Http = http;
        }
    }
}