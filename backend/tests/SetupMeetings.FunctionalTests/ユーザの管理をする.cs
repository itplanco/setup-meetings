using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using SetupMeetings.WebApi;
using System;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class ���[�U�̊Ǘ�������
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
        public void ���[�U��V�K�ɍ쐬���폜����()
        {
            ���[�U��ǉ�����();
            ���[�U�̃p�X���[�h��ύX����();
            ���[�U�̏�����ύX����();
            ���[�U���폜����();
        }

        private void ���[�U��ǉ�����()
        {
            throw new NotImplementedException();
        }

        private void ���[�U�̃p�X���[�h��ύX����()
        {
            throw new NotImplementedException();
        }

        private void ���[�U�̏�����ύX����()
        {
            throw new NotImplementedException();
        }

        private void ���[�U���폜����()
        {
            throw new NotImplementedException();
        }
    }
}
