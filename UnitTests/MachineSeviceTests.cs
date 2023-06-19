using AutoMapper;
using Contracts;
using DormLaundryAPI;
using Entities.DataTransferObjects;
using Entities.Models;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;

namespace UnitTests
{
    public class MachineSeviceTests
    {
        public static IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public void GetAllMachines_ShouldReturnGetAllMachines()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var machineService = new MachineService(repositoryWrapperMock.Object, mapper);

            var result = machineService.GetAllMachinesAsync();

            Assert.Equal(2,result.Result.ToList().Count);
            Assert.IsType<List<MachineDto>>(result.Result);
        }
        [Fact]
        public void GetTurnById_ShouldReturnTurnDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Machine Machine = new() { Id = id, IsActive = true };
            MachineDto MachineDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.Id == id && x.IsActive == true)).ReturnsAsync(Machine);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<MachineDto>(Machine)).Returns(MachineDto);

            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);
            var result = MachineService.GetMachineByIdAsync(id);

            Assert.IsType<Task<MachineDto>>(result);
            Assert.Equal(id, result.Result.Id);

        }
        [Fact]
        public void GetTurnById_WhenTurnIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Machine Machine = new() { Id = id, IsActive = true };
            MachineDto MachineDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.Id == id && x.IsActive == true));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<MachineDto>(Machine)).Returns(MachineDto);
            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.ThrowsAsync<Exception>(() => MachineService.GetMachineByIdAsync(id)).Result;

            Assert.Equal("Makine id'si yok!", exception.Message);

        }
        [Fact]
        public void CreateTurn_WhenStudentHasNotTurn_ShouldReturnTurnDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            MachineDto MachineDto = new() { Id = id };
            Machine Machine = new() { Id = id, IsActive = true };
            MachineForCreationDto MachineForCreation = new();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.No == Machine.No));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Machine>(MachineForCreation)).Returns(Machine);
            mockMapper.Setup(x => x.Map<MachineDto>(Machine)).Returns(MachineDto);
            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);

            var result = MachineService.CreateMachineAsync(MachineForCreation);

            Assert.IsType<Task<MachineDto>>(result);
            Assert.Equal(id, result.Result.Id);
        }
        [Fact]
        public void CreateMachine_WhenMachineHasSameNo_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            MachineDto MachineDto = new() { Id = id };
            Machine Machine = new() { Id = id, IsActive = true, };
            MachineForCreationDto MachineForCreation = new();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.No == Machine.No)).ReturnsAsync(Machine);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Machine>(MachineForCreation)).Returns(Machine);
            mockMapper.Setup(x => x.Map<MachineDto>(Machine)).Returns(MachineDto);
            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => MachineService.CreateMachineAsync(MachineForCreation).Result);

            Assert.Equal("One or more errors occurred. (Bu numaraya sahip makine mevcut)", exception.Message); 
        }
      /*  [Fact]
        public void DeleteMachine_ShouldReturnGuid()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto machineForDeleting = new() { Id = id };
            Machine Machine = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.Id == machineForDeleting.Id && x.IsActive == true)).ReturnsAsync(Machine);
            repositoryWrapper.Setup(x => x.Machine.DeleteAsync(Machine));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Machine>(machineForDeleting)).Returns(Machine);
            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);

            var result = MachineService.DeleteMachineAsync(machineForDeleting).Result;

            Assert.IsType<Guid>(result);
            Assert.Equal(id, result);

        }

        [Fact]
        public void DeleteMachine_WhenMachineIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto machineForDeleting = new() { Id = id, };
            Machine Machine = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Machine.FindAsync(x => x.Id == machineForDeleting.Id));
            repositoryWrapper.Setup(x => x.Machine.DeleteAsync(Machine));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Machine>(machineForDeleting)).Returns(Machine);

            var MachineService = new MachineService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => MachineService.DeleteMachineAsync(machineForDeleting).Result);
            Assert.Equal("One or more errors occurred. (Makine yok!)", exception.Message);
        }*/
    }
}
