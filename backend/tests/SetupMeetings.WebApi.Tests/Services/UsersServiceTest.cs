using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SetupMeetings.Commands.Users;
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
            var mockEventAwaiter = new Mock<IEventAwaiter>();
            mockEventBus.Setup(moq => moq.Send(It.IsAny<Envelope<ICommand>>()));
            mockEventAwaiter.Setup(moq => moq.WaitForMessage<UserCreatedEvent>(It.IsAny<Guid>(), It.IsAny<TimeSpan>()))
                .Returns(Task.FromResult(new UserCreatedEvent() { SourceId = Guid.NewGuid() }));
            var sut = new UsersService(mockEventBus.Object, mockEventAwaiter.Object, null);
            var task = sut.Create(new CreateUserCommand());
            task.Wait(TimeSpan.FromMilliseconds(500));
            mockEventBus.VerifyAll();
        }
    }
}
