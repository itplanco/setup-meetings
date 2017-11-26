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
    public class ユーザの管理をする
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
        public void ユーザを新規に作成し削除する()
        {
            ユーザを追加する();
            ユーザのメールアドレスを変更する();
            ユーザの所属を変更する();
            ユーザを削除する();
        }

        private void ユーザを追加する()
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

        private void ユーザのメールアドレスを変更する()
        {
            _client.Post($"/api/users/{userId}", new ChangeEmailAddressRequest() { NewEmailAddress = "test2@example.com" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertObjectWithStatus<UserResponse>(
                HttpStatusCode.OK,
                m => m.EmailAddress == "test2@example.com");
        }

        private void ユーザの所属を変更する()
        {
            _client.Put($"/api/users/{userId}", new ChangeOrganizationRequest() { NewOrganizationId = "2" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertObjectWithStatus<UserResponse>(
                HttpStatusCode.OK,
                m => m.OrganizationName == "Organization Name 2");
        }

        private void ユーザを削除する()
        {
            _client.Delete($"/api/users/{userId}");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Get($"/api/users/{userId}");
            _client.AssertStatusCode(HttpStatusCode.NotFound);
        }
    }
}
