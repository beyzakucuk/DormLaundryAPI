using Contracts;
using Entities.Models;
using Moq;

namespace UnitTests.Mocks
{
    public class MockIStudentRepository
    {
        protected MockIStudentRepository()
        {
        }

        public static Mock<IStudentRepository> GetMock()
        {
            var mock = new Mock<IStudentRepository>();

            var students = new List<Student>() {
            new Student()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                IsActive = true,
            },

            new Student()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500"),
                IsActive = true,
            }
            };
            mock.Setup(x => x.FindAllAsync()).ReturnsAsync(students);
            return mock;
        }
    }
}
