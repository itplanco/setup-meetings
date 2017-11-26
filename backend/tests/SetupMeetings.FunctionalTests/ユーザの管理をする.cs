using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using SetupMeetings.WebApi;
using SetupMeetings.WebApi.Models.Users;
using System;
using System.Net;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class ���[�U�̊Ǘ�������
    {
        private TestServer _server;
        private HttpClientWrapper _client;
        private string userId;

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
            ���[�U�̃��[���A�h���X��ύX����();
            ���[�U�̏�����ύX����();
            ���[�U���폜����();
        }

        private void ���[�U��ǉ�����()
        {
            var user = new CreateNewUserRequest()
            {
                Name = "Test Name",
                EmailAddress = "test@example.com",
            };
            _client.Post("/api/users", user);
            _client.AssertCreatedStatusCode(out var location);
            var split = location.AbsolutePath.Split('/');
            userId = split[split.Length - 1];
        }

        private void ���[�U�̃��[���A�h���X��ύX����()
        {
            _client.Post($"/api/users/{userId}", new ChangeEmailAddressRequest() { NewEmailAddress = "test2@example.com" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertObjectWithStatus<UserResponse>(
                HttpStatusCode.OK,
                m => m.EmailAddress == "test2@example.com");
        }

        private void ���[�U�̏�����ύX����()
        {
            _client.Put($"/api/users/{userId}", new ChangeOrganizationRequest() { NewOrganizationId = "2" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertObjectWithStatus<UserResponse>(
                HttpStatusCode.OK,
                m => m.OrganizationName == "Organization Name 2");
        }

        private void ���[�U���폜����()
        {
            _client.Delete($"/api/users/{userId}");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertStatusCode(HttpStatusCode.NotFound);
        }
    }
}
