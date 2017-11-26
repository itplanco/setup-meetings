using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetupMeetings.FunctionalTests.Drivers;
using SetupMeetings.WebApi;
using SetupMeetings.WebApi.Models.Meetings;
using System;
using System.Linq;
using System.Net;

namespace SetupMeetings.FunctionalTests
{
    [TestClass]
    public class _3社で忘年会を開く
    {
        private const string MEETING_ID = "1";

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

        private void 忘年会を作成する()
        {
            var newMeeting = new CreateNewMeetingRequest();
            newMeeting.Name = "忘年会";
            newMeeting.OrganizerUserId = "organizer1";
            _client.Post("/api/meetings", newMeeting);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 忘年会が作成されたことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Name == "忘年会" &&
                    m.Organizers[0].UserId == "organizer1");
        }

        private void 忘年会にスポンサーを追加する()
        {
            _client.Post($"/api/meetings/{MEETING_ID}/sponsors/", new CreateNewSponsorRequest() { UserId = "sponsor1" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/sponsors/", new CreateNewSponsorRequest() { UserId = "sponsor2" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 忘年会にスポンサーが追加される()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Organizers.Count == 2 &&
                    m.Organizers[0].UserName == "スポンサー1");
        }

        private void 招待者を6人追加する()
        {
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee1" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee2" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee3" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee4" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee5" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/invitees/", new CreateNewInviteeRequest() { UserId = "invitee6" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 招待者が返信なしで追加されたことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Organizers.Count == 1 &&
                    m.Invitees.Count == 6 &&
                    !m.Invitees.Any(invitee => invitee.Rsvp));
        }

        private void 招待者が不参加の返信をする()
        {
            var rsvpNo = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee1", rsvpNo);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 不参加の返信をした招待者が不参加になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "招待者1")
                        .First()
                        .Rsvp == false &&
                    !m.Attendees.Any());
        }

        private void 招待者が参加の返信をする()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 参加の返信をした招待者が参加者になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "招待者2")
                        .First()
                        .Rsvp == true &&
                    m.Attendees.Any());
        }

        private void 残りの招待者が参加の返信をする()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = true };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee3", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee4", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee5", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee6", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 残りの招待者が参加者になっていることを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName != "招待者1")
                        .Count() == 5 &&
                    m.Attendees.Count() == 5);
        }

        private void 飛び込みの参加者を登録する()
        {
            _client.Post($"/api/meetings/{MEETING_ID}/attendees/", new CreateNewAttendeeRequest() { UserId = "attendee1" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 飛び込みの参加者が登録されたことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Count() == 6);
        }

        private void 参加者が参加をキャンセルする()
        {
            _client.Delete($"/api/meetings/{MEETING_ID}/attendees/invitee3");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 参加者が参加をキャンセルされたことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
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
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee4/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 参加者が忘年会に出席したことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Any(attendee => attendee.UserName == "招待者4" && attendee.Attend));
        }

        private void 参加者が忘年会の支払いをする()
        {
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee4/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 参加者が忘年会の支払いをしたことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Any(attendee => attendee.UserName == "招待者4" && attendee.Paid));
        }

        private void 残りの参加者が忘年会に出席する()
        {
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee3/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee3/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee5/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee5/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/attendee1/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/attendee1/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 残りの参加者が忘年会に出席したことを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Count(attendee => attendee.Attend && attendee.Paid) == 5);
        }

        private void 忘年会の費用を登録する()
        {
            var total = new MeetingPaymentRequest() { TotalPrice = 30000 };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void 参加者全員分忘年会の費用が計算されていることを確認する()
        {
            _client.Get($"/api/meetings/{MEETING_ID}/payments");
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
