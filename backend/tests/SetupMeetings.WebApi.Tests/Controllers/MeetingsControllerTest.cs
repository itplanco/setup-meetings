using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SetupMeetings.Queries.Meetings;

namespace SetupMeetings.WebApi.Controllers
{
    [TestClass]
    public class MeetingsControllerTest
    {
        [TestMethod]
        public void GetMeeting_指定されたIDの会が存在する場合に会のデータが返却される()
        {
            var moq = new Mock<IMeetingsRepository>();
            moq.Setup(m => m.FindById("1")).Returns(new Meeting()
            {
                MeetingId = "meeting1",
                Name = "Meeting Name 1",
            });
            var sut = new MeetingsController();
            var actual = sut.GetMeeting("1");
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetMeeting_指定されたIDの会が存在しない場合にNotFoundとなる()
        {
            var sut = new MeetingsController();
            var actual = sut.GetMeeting("0");
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
    }
}
