using Contracts;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    internal class MockIMachineRepository
    {
        protected MockIMachineRepository()
        {
        }
        public static Mock<IMachineRepository> GetMock()
        {
            var mock = new Mock<IMachineRepository>();

            var machines = new List<Machine>() {
            new Machine()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                IsActive = true,
            },

            new Machine()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500"),
                IsActive = true,
            }
            };
            mock.Setup(x => x.FindAllAsync()).ReturnsAsync(machines);
            return mock;
        }
    }
}
