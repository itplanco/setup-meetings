using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.WebApi;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace SetupMeetings.FunctionalTests.Drivers
{
    class ServerDriver
    {
        private TestServer _server;
        private readonly BlockingCollection<HttpContext> _requests = new BlockingCollection<HttpContext>();

        public ServerDriver()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .Configure(app =>
                {
                    app.Use(next => async context =>
                    {
                        _requests.Add(context);
                        await next.Invoke(context);
                    });
                }));
        }

        public void ShouldReceiveMessage(Action<HttpContext> assertion)
        {
            var context = default(HttpContext);
            if (!_requests.TryTake(out context, TimeSpan.FromSeconds(10)))
            {
                Assert.Fail("レスポンスが来ていない");
            }
            assertion.Invoke(context);
        }

        public HttpClient CreateClient()
        {
            return _server.CreateClient();
        }
    }
}
