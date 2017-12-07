using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SetupMeetings.Queries.Meetings;
using SetupMeetings.WebApi.Services;
using System;

namespace SetupMeetings.WebApi.Controllers
{
    [TestClass]
    public class MeetingsControllerTest
    {
        [TestMethod]
        public void GetMeeting_指定されたIDの会が存在する場合に会のデータが返却される()
        {
            var guid = Guid.NewGuid();
            var moq = new Mock<IMeetingsService>();
            moq.Setup(m => m.GetMeetingById(guid)).Returns(new Meeting()
            {
                Id = guid,
                Name = "Meeting Name 1",
            });
            var sut = new MeetingsController(moq.Object);
            var actual = sut.GetMeeting(guid.ToString());
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetMeeting_指定されたIDの会が存在しない場合にNotFoundとなる()
        {
            var guid = Guid.NewGuid();
            var moq = new Mock<IMeetingsService>();
            moq.Setup(m => m.GetMeetingById(guid)).Returns(new Meeting()
            {
                Id = guid,
                Name = "Meeting Name 1",
            });
            var sut = new MeetingsController(moq.Object);
            var actual = sut.GetMeeting("0");
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
    }
}
