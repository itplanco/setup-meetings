using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using SetupMeetings.WebApi;
using System;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class ユーザの管理をする
    {
        private TestServer _server;
        private HttpClientWrapper _client;

        [TestInitialize]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = new HttpClientWrapper(_server.CreateClient());
        }

        [TestCleanup]
        public void Teardown()
        {
            _server.Dispose();
        }

        [TestMethod]
        public void ユーザを新規に作成し削除する()
        {
            ユーザを追加する();
            ユーザのパスワードを変更する();
            ユーザの所属を変更する();
            ユーザを削除する();
        }

        private void ユーザを追加する()
        {
            throw new NotImplementedException();
        }

        private void ユーザのパスワードを変更する()
        {
            throw new NotImplementedException();
        }

        private void ユーザの所属を変更する()
        {
            throw new NotImplementedException();
        }

        private void ユーザを削除する()
        {
            throw new NotImplementedException();
        }
    }
}
