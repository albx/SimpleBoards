using Bunit;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SimpleBoards.Web.App.Test.Http.Mocks
{
    public static class HttpClientMockExtensions
    {
        public static MockHttpMessageHandler AddHttpClientMock(this TestServiceProvider services)
        {
            var httpHandler = new MockHttpMessageHandler();
            var client = httpHandler.ToHttpClient();
            client.BaseAddress = new Uri("https://localhost");

            services.AddSingleton(client);
            return httpHandler;
        }

        public static MockedRequest RespondAsJson<T>(this MockedRequest request, T content)
        {
            request.Respond(r =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonSerializer.Serialize(content));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return response;
            });

            return request;
        }

        public static MockedRequest RespondWithStatusCode(this MockedRequest request, HttpStatusCode statusCode)
        {
            request.Respond(r =>
            {
                var response = new HttpResponseMessage(statusCode);
                return response;
            });

            return request;
        }
    }
}
