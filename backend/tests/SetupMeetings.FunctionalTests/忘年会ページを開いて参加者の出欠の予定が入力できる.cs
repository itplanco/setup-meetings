using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class �Y�N��y�[�W���J���ĎQ���҂̏o���̗\�肪���͂ł���
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
        public void �����O�C����ԂŖY�N��y�[�W���J���ƃ��O�C�������ۂ����()
        {
            _client.RequestUrl("/api/meetings/1");
            _server.ShouldReceiveMessage(m => Assert.IsTrue(m.Request.Path.StartsWithSegments(PathString.FromUriComponent("/api/meetings/1"))));
            _client.ShouldRejected();
        }

        [TestMethod]
        public void ���O�C����ԂŖY�N��y�[�W���J���ƈꗗ��\�����邱�Ƃ��ł���()
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
