﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SetupMeetings.FunctionalTests.Drivers
{
    class HttpClientWrapper
    {
        private HttpClient httpClient;
        private readonly BlockingCollection<HttpResponseMessage> responseMessages = new BlockingCollection<HttpResponseMessage>();

        public HttpClientWrapper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void Get(string uri)
        {
            httpClient.GetAsync(uri).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void Post<T>(string uri, T obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpClient.PostAsync(uri, content).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void Put(string uri)
        {
            httpClient.PutAsync(uri, null).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void Put<T>(string uri, T obj)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpClient.PutAsync(uri, content).ContinueWith(t => responseMessages.Add(t.Result));
        }

        public void Delete(string uri)
        {
            httpClient.DeleteAsync(uri).ContinueWith(t => responseMessages.Add(t.Result));
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

        public void AssertObjectWithStatus<T>(HttpStatusCode status, Predicate<T> pred)
        {
            var message = TakeMessage();
            T obj = JsonConvert.DeserializeObject<T>(message.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(pred.Invoke(obj));
            Assert.AreEqual(status, message.StatusCode);
        }

        public void AssertStatusCode(HttpStatusCode status)
        {
            var message = TakeMessage();
            Assert.AreEqual(status, message.StatusCode);
        }
    }
}
