using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Moq;
using Services;

namespace UnitTests.Mocks
{
    internal class MockITurnRepository
    {
       protected MockITurnRepository()
        {

        }
        public static Mock<ITurnRepository> GetMock()
        {
            var mock = new Mock<ITurnRepository>();
            var turns = new List<Turn>()
            {
            new Turn()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                IsActive = true,
                Date = DateTime.UtcNow.AddDays(1)
            },
            new Turn()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500"),
                IsActive = true,
                Date = DateTime.UtcNow.AddDays(1)
            }
            };
            mock.Setup(m => m.FindAllAsync()).ReturnsAsync(turns);
           
            return mock;
        }
    }
}
