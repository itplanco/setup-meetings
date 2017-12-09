using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SetupMeetings.Commands.Users;
using SetupMeetings.Common.Events.Users;
using SetupMeetings.Infrastructure.Messaging;
using System;
using System.Threading.Tasks;

namespace SetupMeetings.WebApi.Services
{
    [TestClass]
    public class UsersServiceTest
    {
        [TestMethod]
        public void ユーザサービスにCreateUserCommandを処理させてCommandBusにコマンドが送られる()
        {
            var mockEventBus = new Mock<ICommandBus>();
            mockEventBus.Setup(moq => moq.Send(It.IsAny<Envelope<ICommand>>()));
            var sut = new UsersService(mockEventBus.Object, null);
            var task = sut.Create(new CreateUserCommand());
            task.Wait(TimeSpan.FromMilliseconds(500));
            mockEventBus.VerifyAll();
        }
    }
}
