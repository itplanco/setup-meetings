using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using SetupMeetings.WebApi;
using SetupMeetings.WebApi.Models.Meetings;
using SetupMeetings.WebApi.Models.Users;
using System;
using System.Linq;
using System.Net;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class _3社で忘年会を開く
    {
        private string MeetingId;
        private string OrganizerId1;
        private string SponsorId1;
        private string SponsorId2;
        private string InviteeId1;
        private string InviteeId2;
        private string InviteeId3;
        private string InviteeId4;
        private string InviteeId5;
        private string InviteeId6;

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
        public void 忘年会を作成して実施する()
        {
            必要なユーザを作成する();
            忘年会を作成する();
            忘年会が作成されたことを確認する();
            忘年会にスポンサーを追加する();
            忘年会にスポンサーが追加される();
            招待者を6人追加する();
            招待者が返信なしで追加されたことを確認する();
            招待者が不参加の返信をする();
            不参加の返信をした招待者が不参加になっていることを確認する();
            招待者が参加の返信をする();
            参加の返信をした招待者が参加者になっていることを確認する();
            残りの招待者が参加の返信をする();
            残りの招待者が参加者になっていることを確認する();
            飛び込みの参加者を登録する();
            飛び込みの参加者が登録されたことを確認する();
            参加者が参加をキャンセルする();
            参加者が参加をキャンセルされたことを確認する();
            //忘年会を開催する();
            //忘年会が開催になったことを確認する();
            参加者が忘年会に出席する();
            参加者が忘年会に出席したことを確認する();
            残りの参加者が忘年会に出席する();
            残りの参加者が忘年会に出席したことを確認する();
            忘年会の費用を登録する();
            参加者全員分忘年会の費用が計算されていることを確認する();
        }

        private void 必要なユーザを作成する()
        {
            Func<Uri, string> id = uri =>
            {
                var split = uri.AbsolutePath.Split('/');
                return split[split.Length - 1];
            };
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Organizer1", EmailAddress = "organizer1@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var l1);
            OrganizerId1 = id(l1);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Sponsor1", EmailAddress = "sponsor1@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var l2);
            SponsorId1 = id(l2);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Sponsor2", EmailAddress = "sponsor2@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var l3);
            SponsorId2 = id(l3);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee1", EmailAddress = "invitee1@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il1);
            InviteeId1= id(il1);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee2", EmailAddress = "invitee2@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il2);
            InviteeId2= id(il2);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee3", EmailAddress = "invitee3@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il3);
            InviteeId3= id(il3);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee4", EmailAddress = "invitee4@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il4);
            InviteeId4= id(il4);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee5", EmailAddress = "invitee5@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il5);
            InviteeId5= id(il5);
            _client.Post("/api/users/", new CreateNewUserRequest() { Name = "Invitee6", EmailAddress = "invitee6@test.com", OrganizationId = Guid.NewGuid().ToString() });
            _client.AssertCreatedStatusCode(out var il6);
            InviteeId6= id(il6);
        }

        private void 忘年会を作成する()
        {
            var newMeeting = new CreateNewMeetingRequest();
            newMeeting.Name = "忘年会";
            newMeeting.OrganizerId = OrganizerId1;
            _client.Post("/api/meetings", newMeeting);
            _client.AssertCreatedStatusCode(out var location);
            var split = location.AbsolutePath.Split('/');
            MeetingId = split[split.Length - 1];
        }

        private void 忘年会が作成されたことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Name == "忘年会" &&
                    m.Organizers[0].UserId == OrganizerId1);
        }

        private void 忘年会にスポンサーを追加する()
        {
            _client.Post($"/api/meetings/{MeetingId}/sponsors/", new CreateNewSponsorRequest() { UserId = SponsorId1 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/sponsors/", new CreateNewSponsorRequest() { UserId = SponsorId2 });
            _client.AssertStatusCode(HttpStatusCode.Created);
        }

        private void 忘年会にスポンサーが追加される()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Sponsors.Count == 2 &&
                    m.Sponsors[0].UserName == "Sponsor1");
            _client.Get($"/api/meetings/{MeetingId}/sponsors");
            _client.AssertObjectWithStatus<SponsorsResponse>(
                HttpStatusCode.OK,
                m =>
                    m.Sponsors.Count == 2 &&
                    m.Sponsors[1].UserName == "Sponsor2");
        }

        private void 招待者を6人追加する()
        {
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId1 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId2 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId3 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId4 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId5 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/invitees/", new CreateNewInviteeRequest() { UserId = InviteeId6 });
            _client.AssertStatusCode(HttpStatusCode.Created);
        }

        private void 招待者が返信なしで追加されたことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Organizers.Count == 1 &&
                    m.Invitees.Count == 6 &&
                    !m.Invitees.Any(invitee => invitee.Rsvp));
            _client.Get($"/api/meetings/{MeetingId}/invitees");
            _client.AssertObjectWithStatus<InviteesResponse>(
                HttpStatusCode.OK,
                m =>
                    m.Invitees.Count == 6 &&
                    !m.Invitees.Any(invitee => invitee.Rsvp));
        }

        private void 招待者が不参加の返信をする()
        {
            var rsvpNo = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId1}", rsvpNo);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 不参加の返信をした招待者が不参加になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "招待者1")
                        .First()
                        .Rsvp == false &&
                    !m.Attendees.Any());
        }

        private void 招待者が参加の返信をする()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 参加の返信をした招待者が参加者になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "招待者2")
                        .First()
                        .Rsvp == true &&
                    m.Attendees.Any());
        }

        private void 残りの招待者が参加の返信をする()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = true };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId3}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId4}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId5}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId6}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 残りの招待者が参加者になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName != "招待者1")
                        .Count() == 5 &&
                    m.Attendees.Count() == 5);
        }

        private void 飛び込みの参加者を登録する()
        {
            _client.Post($"/api/meetings/{MeetingId}/attendees/", new CreateNewAttendeeRequest() { UserId = "attendee1" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 飛び込みの参加者が登録されたことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Count() == 6);
        }

        private void 参加者が参加をキャンセルする()
        {
            _client.Delete($"/api/meetings/{MeetingId}/attendees/{InviteeId3}");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 参加者が参加をキャンセルされたことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Count() == 5 &&
                    !m.Attendees.Any(attendee => attendee.UserName == "招待者3") &&
                    m.Invitees.Any(invitee => invitee.UserName == "招待者3"));
        }

        private void 忘年会を開催する()
        {
            throw new NotImplementedException();
        }

        private void 忘年会が開催になったことを確認する()
        {
            throw new NotImplementedException();
        }

        private void 参加者が忘年会に出席する()
        {
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId4}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 参加者が忘年会に出席したことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Any(attendee => attendee.UserName == "招待者4" && attendee.Attend));
        }

        private void 参加者が忘年会の支払いをする()
        {
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId4}/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 参加者が忘年会の支払いをしたことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Any(attendee => attendee.UserName == "招待者4" && attendee.Paid));
        }

        private void 残りの参加者が忘年会に出席する()
        {
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId3}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId3}/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId5}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId5}/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/attendee1/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
            _client.Put($"/api/meetings/{MeetingId}/invitees/attendee1/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 残りの参加者が忘年会に出席したことを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Count(attendee => attendee.Attend && attendee.Paid) == 5);
        }

        private void 忘年会の費用を登録する()
        {
            var total = new UpdatePaymentInfoRequest() { TotalPrice = 30000 };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void 参加者全員分忘年会の費用が計算されていることを確認する()
        {
            _client.Get($"/api/meetings/{MeetingId}/payments");
            _client.AssertObjectWithStatus<MeetingPaymentResponse>(
                HttpStatusCode.OK,
                m =>
                    m.TotalPrice == 2000 &&
                    m.Details
                        .Where(detail => detail.UserId.StartsWith("sponsor"))
                        .Count(detail => detail.Price == 5000) == 2);
        }
    }
}
