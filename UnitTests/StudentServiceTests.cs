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
    public class StudentServiceTests
    {
        public static IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public void GetAllStudents_ShouldReturnGetAllStudents()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var studentService = new StudentService(repositoryWrapperMock.Object, mapper);

            var result = studentService.GetAllStudentsAsync();

            Assert.Equal(2,result.Result.ToList().Count);
            Assert.IsType<List<StudentDto>>(result.Result);
        }
        [Fact]
        public void GetStudentById_ShouldReturnStudentDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Student Student = new() { Id = id, IsActive = true };
            StudentDto StudentDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Student.FindAsync(x => x.Id == id && x.IsActive == true)).ReturnsAsync(Student);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<StudentDto>(Student)).Returns(StudentDto);

            var StudentService = new StudentService(repositoryWrapper.Object, mockMapper.Object);
            var result = StudentService.GetStudentByIdAsync(id);

            Assert.IsType<Task<StudentDto>>(result);
            Assert.Equal(id, result.Result.Id);

        }
        [Fact]
        public void GetStudentById_WhenTurnIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Student Student = new() { Id = id, IsActive = true };
            StudentDto StudentDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Student.FindAsync(x => x.Id == id && x.IsActive == true));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<StudentDto>(Student)).Returns(StudentDto);
            var StudentService = new StudentService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => StudentService.GetStudentByIdAsync(id).Result);

            Assert.Equal("One or more errors occurred. (Öğrenci id'si yok!)", exception.Message);

        }
        [Fact]
        public void CreateStudent_ShouldReturnStudentDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            StudentDto StudentDto = new() { Id = id };
            Student Student = new() { Id = id, IsActive = true };

            StudentForCreationDto studentForCreation = new();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Student.CreateAsync(Student));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Student>(studentForCreation)).Returns(Student);
            mockMapper.Setup(x => x.Map<StudentDto>(Student)).Returns(StudentDto);
            var StudentService = new StudentService(repositoryWrapper.Object, mockMapper.Object);

            var result = StudentService.CreateStudentAsync(studentForCreation);

            Assert.IsType<Task<StudentDto>>(result);
            Assert.Equal(id, result.Result.Id);
        }
        [Fact]
        public void DeleteStudent_ShouldReturnGuid()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto studentForDeleting = new() { Id = id };
            Student Student = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Student.FindAsync(x => x.Id == studentForDeleting.Id && x.IsActive == true)).ReturnsAsync(Student);
            repositoryWrapper.Setup(x => x.Student.DeleteAsync(Student));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Student>(studentForDeleting)).Returns(Student);
            var StudentService = new StudentService(repositoryWrapper.Object, mockMapper.Object);

            var result = StudentService.DeleteStudentAsync(studentForDeleting).Result;

            Assert.IsType<Guid>(result);
            Assert.Equal(id, result);

        }

        [Fact]
        public void DeleteTurn_WhenTurnIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto studentForDeleting = new() { Id = id, };
            Student Student = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Student.FindAsync(x => x.Id == studentForDeleting.Id));
            repositoryWrapper.Setup(x => x.Student.DeleteAsync(Student));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Student>(studentForDeleting)).Returns(Student);

            var StudentService = new StudentService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => StudentService.DeleteStudentAsync(studentForDeleting).Result);
            Assert.Equal("One or more errors occurred. (Öğrenci yok!)", exception.Message);
        }
    }
}
