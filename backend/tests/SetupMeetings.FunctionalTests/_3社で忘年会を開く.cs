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
    public class _3�ЂŖY�N����J��
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
        public void �Y�N����쐬���Ď��{����()
        {
            �K�v�ȃ��[�U���쐬����();
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

        private void �K�v�ȃ��[�U���쐬����()
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

        private void �Y�N����쐬����()
        {
            var newMeeting = new CreateNewMeetingRequest();
            newMeeting.Name = "�Y�N��";
            newMeeting.OrganizerId = OrganizerId1;
            _client.Post("/api/meetings", newMeeting);
            _client.AssertCreatedStatusCode(out var location);
            var split = location.AbsolutePath.Split('/');
            MeetingId = split[split.Length - 1];
        }

        private void �Y�N��쐬���ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Name == "�Y�N��" &&
                    m.Organizers[0].UserId == OrganizerId1);
        }

        private void �Y�N��ɃX�|���T�[��ǉ�����()
        {
            _client.Post($"/api/meetings/{MeetingId}/sponsors/", new CreateNewSponsorRequest() { UserId = SponsorId1 });
            _client.AssertStatusCode(HttpStatusCode.Created);
            _client.Post($"/api/meetings/{MeetingId}/sponsors/", new CreateNewSponsorRequest() { UserId = SponsorId2 });
            _client.AssertStatusCode(HttpStatusCode.Created);
        }

        private void �Y�N��ɃX�|���T�[���ǉ������()
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

        private void ���Ҏ҂�6�l�ǉ�����()
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

        private void ���Ҏ҂��ԐM�Ȃ��Œǉ����ꂽ���Ƃ��m�F����()
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

        private void ���Ҏ҂��s�Q���̕ԐM������()
        {
            var rsvpNo = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId1}", rsvpNo);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �s�Q���̕ԐM���������Ҏ҂��s�Q���ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "���Ҏ�1")
                        .First()
                        .Rsvp == false &&
                    !m.Attendees.Any());
        }

        private void ���Ҏ҂��Q���̕ԐM������()
        {
            var rsvpYes = new InviteeRespondToRsvpRequest() { Response = false };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}", rsvpYes);
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �Q���̕ԐM���������Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName == "���Ҏ�2")
                        .First()
                        .Rsvp == true &&
                    m.Attendees.Any());
        }

        private void �c��̏��Ҏ҂��Q���̕ԐM������()
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

        private void �c��̏��Ҏ҂��Q���҂ɂȂ��Ă��邱�Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Invitees
                        .Where(invitee => invitee.UserName != "���Ҏ�1")
                        .Count() == 5 &&
                    m.Attendees.Count() == 5);
        }

        private void ��э��݂̎Q���҂�o�^����()
        {
            _client.Post($"/api/meetings/{MeetingId}/attendees/", new CreateNewAttendeeRequest() { UserId = "attendee1" });
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void ��э��݂̎Q���҂��o�^���ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Count() == 6);
        }

        private void �Q���҂��Q�����L�����Z������()
        {
            _client.Delete($"/api/meetings/{MeetingId}/attendees/{InviteeId3}");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �Q���҂��Q�����L�����Z�����ꂽ���Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
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
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId4}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Any(attendee => attendee.UserName == "���Ҏ�4" && attendee.Attend));
        }

        private void �Q���҂��Y�N��̎x����������()
        {
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId4}/paid");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �Q���҂��Y�N��̎x�������������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Any(attendee => attendee.UserName == "���Ҏ�4" && attendee.Paid));
        }

        private void �c��̎Q���҂��Y�N��ɏo�Ȃ���()
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

        private void �c��̎Q���҂��Y�N��ɏo�Ȃ������Ƃ��m�F����()
        {
            _client.Get($"/api/meetings/{MeetingId}");
            _client.AssertObjectWithStatus<MeetingResponse>(
                HttpStatusCode.OK,
                m =>
                    m.MeetingId == MeetingId &&
                    m.Attendees.Count(attendee => attendee.Attend && attendee.Paid) == 5);
        }

        private void �Y�N��̔�p��o�^����()
        {
            var total = new UpdatePaymentInfoRequest() { TotalPrice = 30000 };
            _client.Put($"/api/meetings/{MeetingId}/invitees/{InviteeId2}/attend");
            _client.AssertStatusCode(HttpStatusCode.Accepted);
        }

        private void �Q���ґS�����Y�N��̔�p���v�Z����Ă��邱�Ƃ��m�F����()
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
