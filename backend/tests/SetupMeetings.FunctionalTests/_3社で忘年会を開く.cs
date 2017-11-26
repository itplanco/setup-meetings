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
    public class _3�ЂŖY�N����J��
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
        public void �Y�N����쐬���Ď��{����()
        {
            �Y�N����쐬����();
            �Y�N��쐬���ꂽ���Ƃ��m�F����();
            �Y�N��ɃX�|���T�[��ǉ�����();
            �Y�N��ɃX�|���T�[���ǉ������();
            ���Ҏ҂�6�l�ǉ�����();
            ���Ҏ҂��ԐM�Ȃ��Œǉ����ꂽ���Ƃ��m�F����();
            ���Ҏ҂��s�Q���̕ԐM������();
            �s�Q���̕ԐM���������Ҏ҂��s�Q���ɂȂ��Ă��邱�Ƃ��m�F����();
            ���Ҏ҂��Q���̕ԐM������();
            �Q���̕ԐM���������Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����();
            �c��̏��Ҏ҂��Q���̕ԐM������();
            �c��̏��Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����();
            ��э��݂̎Q���҂�o�^����();
            ��э��݂̎Q���҂��o�^���ꂽ���Ƃ��m�F����();
            �Q���҂��Q�����L�����Z������();
            �Q���҂��Q�����L�����Z�����ꂽ���Ƃ��m�F����();
            //�Y�N����J�Â���();
            //�Y�N��J�ÂɂȂ������Ƃ��m�F����();
            �Q���҂��Y�N��ɏo�Ȃ���();
            �Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����();
            �c��̎Q���҂��Y�N��ɏo�Ȃ���();
            �c��̎Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����();
            �Y�N��̔�p��o�^����();
            �Q���ґS�����Y�N��̔�p���v�Z����Ă��邱�Ƃ��m�F����();
        }

        private void �Y�N����쐬����()
        {
            var newMeeting = new CreateNewMeetingRequest();
            newMeeting.Name = "�Y�N��";
            newMeeting.OrganizerUserId = "organizer1";
            _client.Post("/api/meetings", newMeeting);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Y�N��쐬���ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Name == "�Y�N��" &&
                    m.Organizers[0].UserId == "organizer1");
        }

        private void �Y�N��ɃX�|���T�[��ǉ�����()
        {
            _client.Post($"/api/meetings/{MEETING_ID}/sponsors/", new CreateNewSponsorRequest() { UserId = "sponsor1" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
            _client.Post($"/api/meetings/{MEETING_ID}/sponsors/", new CreateNewSponsorRequest() { UserId = "sponsor2" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Y�N��ɃX�|���T�[���ǉ������()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Organizers.Count == 2 &&
                    m.Organizers[0].UserName == "�X�|���T�[1");
        }

        private void ���Ҏ҂�6�l�ǉ�����()
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

        private void ���Ҏ҂��ԐM�Ȃ��Œǉ����ꂽ���Ƃ��m�F����()
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

        private void ���Ҏ҂��s�Q���̕ԐM������()
        {
            var rsvpNo = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee1", rsvpNo);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �s�Q���̕ԐM���������Ҏ҂��s�Q���ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "���Ҏ�1")
                        .First()
                        .Rsvp == false &&
                    !m.Attendees.Any());
        }

        private void ���Ҏ҂��Q���̕ԐM������()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Q���̕ԐM���������Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "���Ҏ�2")
                        .First()
                        .Rsvp == true &&
                    m.Attendees.Any());
        }

        private void �c��̏��Ҏ҂��Q���̕ԐM������()
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

        private void �c��̏��Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Invitees
                        .Where(invitee => invitee.UserName != "���Ҏ�1")
                        .Count() == 5 &&
                    m.Attendees.Count() == 5);
        }

        private void ��э��݂̎Q���҂�o�^����()
        {
            _client.Post($"/api/meetings/{MEETING_ID}/attendees/", new CreateNewAttendeeRequest() { UserId = "attendee1" });
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void ��э��݂̎Q���҂��o�^���ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Count() == 6);
        }

        private void �Q���҂��Q�����L�����Z������()
        {
            _client.Delete($"/api/meetings/{MEETING_ID}/attendees/invitee3");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Q���҂��Q�����L�����Z�����ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Count() == 5 &&
                    !m.Attendees.Any(attendee => attendee.UserName == "���Ҏ�3") &&
                    m.Invitees.Any(invitee => invitee.UserName == "���Ҏ�3"));
        }

        private void �Y�N����J�Â���()
        {
            throw new NotImplementedException();
        }

        private void �Y�N��J�ÂɂȂ������Ƃ��m�F����()
        {
            throw new NotImplementedException();
        }

        private void �Q���҂��Y�N��ɏo�Ȃ���()
        {
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee4/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Any(attendee => attendee.UserName == "���Ҏ�4" && attendee.Attend));
        }

        private void �Q���҂��Y�N��̎x����������()
        {
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee4/paid");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Q���҂��Y�N��̎x�������������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Any(attendee => attendee.UserName == "���Ҏ�4" && attendee.Paid));
        }

        private void �c��̎Q���҂��Y�N��ɏo�Ȃ���()
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

        private void �c��̎Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MEETING_ID}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MEETING_ID &&
                    m.Attendees.Count(attendee => attendee.Attend && attendee.Paid) == 5);
        }

        private void �Y�N��̔�p��o�^����()
        {
            var total = new MeetingPaymentRequest() { TotalPrice = 30000 };
            _client.Put($"/api/meetings/{MEETING_ID}/invitees/invitee2/attend");
            _client.AssertStatusCode(HttpStatusCode.NoContent);
        }

        private void �Q���ґS�����Y�N��̔�p���v�Z����Ă��邱�Ƃ��m�F����()
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
