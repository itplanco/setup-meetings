using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SetupMeetings.Commands;
using SetupMeetings.Infrastructure.Messaging;

namespace SetupMeetings.WebApi.Services
{
    [TestClass]
    public class UsersServiceTest
    {
        [TestMethod]
        public void ユーザサービスにCreateUserCommandを処理させてCommandBusにコマンドが送られる()
        {
            var mock = new Mock<ICommandBus>();
            mock.Setup(moq => moq.Send(It.IsAny<Envelope<ICommand>>()));
            var sut = new UsersService(mock.Object);
            sut.Create(new CreateUserCommand());
            mock.VerifyAll();
        }
    }
}
