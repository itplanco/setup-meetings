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
            _client.RequestUrl("/api/meetings/1");
            _server.ShouldReceiveMessage(m => Assert.IsTrue(m.Request.Path.StartsWithSegments(PathString.FromUriComponent("/api/meetings/1"))));
            _client.ShouldRejected();
        }

        [TestMethod]
        public void ログイン状態で忘年会ページを開くと一覧を表示することができる()
        {
            _client.RequestToken();
            _server.ShouldReceiveMessage(m => Assert.IsTrue(m.Request.Path.StartsWithSegments("/connect/token")));
            _client.ShouldGrant();
            _client.RequestUrl("/api/meetings/1");
            _server.ShouldReceiveMessage(m => Assert.IsTrue(m.Request.Path.StartsWithSegments("/api/meetings/1")));
            _client.ShouldReturnResponse(r => Assert.IsNotNull(r.Content.ReadAsStringAsync().Result));
        }
    }
}
