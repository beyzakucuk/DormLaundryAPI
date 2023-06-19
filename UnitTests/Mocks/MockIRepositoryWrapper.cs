using Contracts;
using Moq;
namespace UnitTests.Mocks
{
    internal static class MockIRepositoryWrapper
    {
        public static Mock<IRepositoryWrapper> GetMock()
        {
            var mock = new Mock<IRepositoryWrapper>();
            var turnRepositoryMock = MockITurnRepository.GetMock();
            var studentRepositoryMock = MockIStudentRepository.GetMock();
            var machineRepositoryMock = MockIMachineRepository.GetMock();
            var adminRepositoryMock = MockIAdminRepository.GetMock();
            mock.Setup(m => m.Turn).Returns(() => turnRepositoryMock.Object);
            mock.Setup(m => m.Student).Returns(() => studentRepositoryMock.Object);
            mock.Setup(m => m.Machine).Returns(() => machineRepositoryMock.Object);
            mock.Setup(m => m.Admin).Returns(() => adminRepositoryMock.Object);
            return mock;
        }
    }
}
