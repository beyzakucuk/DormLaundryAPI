using AutoMapper;
using Contracts;
using DormLaundryAPI;
using Entities.DataTransferObjects;
using Entities.Models;
using Moq;
using Services;
using UnitTests.Mocks;

namespace UnitTests
{
    public class AdminServiceTests
    {
        public static IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public void GetAllAdmins_ShouldReturnGetAllAdmins()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var adminService = new AdminService(repositoryWrapperMock.Object, mapper);

            var result = adminService.GetAllAdminsAsync();

            Assert.Equal( 2 ,result.Result.ToList().Count);
            Assert.IsType<List<AdminDto>>(result.Result);
        }
        [Fact]
        public void GetAdminById_ShouldReturnAdminDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Admin admin = new() { Id = id, IsActive = true };
            AdminDto adminDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x=> x.Admin.FindAsync(x => x.Id == id && x.IsActive == true)).ReturnsAsync(admin);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<AdminDto>(admin)).Returns(adminDto);
            var adminService = new AdminService(repositoryWrapper.Object, mockMapper.Object);

            var result = adminService.GetAdminByIdAsync(id);

            Assert.IsType<Task<AdminDto>>(result);
            Assert.Equal(id, result.Result.Id);

        }
        [Fact]
        public void GetAdminById_WhenAdminIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Admin admin = new() { Id = id, IsActive = true, };
            AdminDto adminDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Admin.FindAsync(x => x.Id == id && x.IsActive == true));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<AdminDto>(admin)).Returns(adminDto);
            var adminService = new AdminService(repositoryWrapper.Object, mockMapper.Object);

            var exception =  Assert.ThrowsAsync<Exception>(() => adminService.GetAdminByIdAsync(id)).Result;

            Assert.Equal("Admin id'si yok!", exception.Message);

        }
        [Fact]
        public void CreateAdmin_ShouldReturnAdmindto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            AdminDto adminDto = new() { Id = id };
            Admin admin = new() { Id = id, IsActive = true };
            AdminForCreationDto adminForCreation = new();

            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Admin.CreateAsync(admin));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Admin>(adminForCreation)).Returns(admin);
            mockMapper.Setup(x => x.Map<AdminDto>(admin)).Returns(adminDto);

            var adminService = new AdminService(repositoryWrapper.Object, mockMapper.Object);
            var result = adminService.CreateAdminAsync(adminForCreation);

            Assert.IsType<Task<AdminDto>>(result);
            Assert.Equal(id, result.Result.Id);
        }
        [Fact]
        public void DeleteAdmin_ShouldReturnGuid()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto adminForDeleting = new() { Id = id };
            Admin admin = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            var mock =repositoryWrapper.Setup(x => x.Admin.FindAsync(x => x.Id == adminForDeleting.Id && x.IsActive == true)).ReturnsAsync(admin);

            repositoryWrapper.Setup(x => x.Admin.DeleteAsync(admin));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Admin>(adminForDeleting)).Returns(admin);
            var adminService = new AdminService(repositoryWrapper.Object, mockMapper.Object);

            var result = adminService.DeleteAdminAsync(adminForDeleting).Result;

            Assert.IsType<Guid>(result);
            Assert.Equal(id, result);

        }

        [Fact]
        public void DeleteAdmin_WhenAdminIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto adminForDeleting = new() { Id = id };
            Admin admin = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Admin.FindAsync(x => x.Id == adminForDeleting.Id));
            repositoryWrapper.Setup(x => x.Admin.DeleteAsync(admin));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Admin>(adminForDeleting)).Returns(admin);

            var adminService = new AdminService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => adminService.DeleteAdminAsync(adminForDeleting)).Result;
            Assert.Equal("Admin yok!", exception.Message);
        }
    }
}
