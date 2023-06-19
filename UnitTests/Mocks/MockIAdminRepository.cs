using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Mocks
{
    internal class MockIAdminRepository
    {
        protected MockIAdminRepository()
        {
        }
        public static Mock<IAdminRepository> GetMock()
        {
            var mock = new Mock<IAdminRepository>();

            var admins = new List<Admin>() {
            new Admin()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                IsActive = true,
            },

            new Admin()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289500"),
                IsActive = true,
            }
            };
            mock.Setup(x => x.FindAllAsync()).ReturnsAsync(admins);
            return mock;
        }
    }
}
