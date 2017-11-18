using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class 忘年会ページを開いて参加者の出欠の予定が入力できる
    {
        private ServerDriver _server;
        private ClientDriver _client;

        [TestInitialize]
        public void Setup()
        {
            _server = new ServerDriver();
            _client = new ClientDriver(_server.CreateClient());
        }

        [TestMethod]
        public void 未ログイン状態で忘年会ページを開くとログインが拒否される()
        {
            _client.RequestUrl("/");
            _server.ShouldReceiveMessage(m => Assert.IsTrue(m.Request.Path.StartsWithSegments(PathString.FromUriComponent("/"))));
            _client.ShouldRejected();
        }
    }
}
