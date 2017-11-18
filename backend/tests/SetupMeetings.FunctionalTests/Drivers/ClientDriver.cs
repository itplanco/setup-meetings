using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SetupMeetings.FunctionalTests.Drivers
{
    class ClientDriver
    {
        private HttpClient httpClient;
        private readonly BlockingCollection<HttpResponseMessage> responseMessages = new BlockingCollection<HttpResponseMessage>();

        public ClientDriver(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void RequestUrl(string uri)
        {
            httpClient.GetAsync(uri).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void RequestToken()
        {
            LoginWith("Bob", "P@ssw0rd").ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void ShouldRejected()
        {
            HttpResponseMessage response = TakeMessage();
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        public void ShouldGrant()
        {
            HttpResponseMessage response = TakeMessage();
            Assert.IsTrue(response.IsSuccessStatusCode);
            var accessToken = JObject.Parse(response.Content.ReadAsStringAsync().Result)["access_token"].Value<string>();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public void ShouldReturnResponse(Action<HttpResponseMessage> assertion)
        {
            HttpResponseMessage response = TakeMessage();
            assertion.Invoke(response);
        }

        public Task<HttpResponseMessage> LoginWith(string username, string password)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });
            return httpClient.PostAsync("/connect/token", content);
        }

        private HttpResponseMessage TakeMessage()
        {
            var response = default(HttpResponseMessage);
            if (!responseMessages.TryTake(out response, TimeSpan.FromSeconds(5)))
            {
                Assert.Fail("レスポンスが戻ってきていない");
            }

            return response;
        }
    }
}
