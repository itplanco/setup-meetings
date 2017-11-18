using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace SetupMeetings.FunctionalTests.Drivers
{
    class ClientDriver
    {
        private HttpClient httpClient;
        private BlockingCollection<HttpResponseMessage> responseMessages;

        public ClientDriver(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void RequestUrl(string uri)
        {
            httpClient.GetAsync(uri).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void ShouldRejected()
        {
            var response = default(HttpResponseMessage);
            if (!responseMessages.TryTake(out response, TimeSpan.FromSeconds(5))) 
            {
                Assert.Fail("レスポンスが戻ってきていない");
            }
            Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);
        }

        public void LoginWith(string username, string password)
        {
            HttpContent content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("password", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });
        }
    }
}
