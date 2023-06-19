using AutoMapper;
using Contracts;
using DormLaundryAPI;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Services;
using UnitTests.Mocks;

namespace UnitTests
{
    public class TurnServiceTests
    {
        public static IMapper GetMapper()
        {
            var mappingProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }
        [Fact]
        public void GetAllTurns_ShouldReturnGetAllTurns()
        {
            var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
            var mapper = GetMapper();
            var turnService = new TurnService(repositoryWrapperMock.Object, mapper);

            var result = turnService.GetAllTurnsAsync();

            Assert.Equal(2, result.Result.ToList().Count);
            Assert.IsType<List<TurnDto>>(result.Result);
        }
        [Fact]
        public void GetTurnsWithDetails_WhenTakeMachineId_ShouldReturnAllFilterTurns()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            TurnFilterDto turnFilterDto = new() { MachineId = id };
            var turns = new List<Turn>() { new Turn() {MachineId = id}, new Turn() { MachineId = id }}.AsQueryable();
            var turnWithDetailDto = new List<TurnWithDetailDto>() { new TurnWithDetailDto(), new TurnWithDetailDto()}.AsEnumerable();

            var turnsOnOnePage = turns.Skip((turnFilterDto.PageNumber - 1) * 3).Take(3);

            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.Find(
                t => (t.MachineId == turnFilterDto.MachineId || turnFilterDto.MachineId == Guid.Empty)
                && (t.StudentId == turnFilterDto.StudentId || turnFilterDto.StudentId == Guid.Empty)
                && ((t.Date.Day == turnFilterDto.Date.Day && (t.Date.Hour == turnFilterDto.Date.Hour +offset.Hours
                || turnFilterDto.Date.Hour <= 7)
                && t.Date.Month == turnFilterDto.Date.Month && t.Date.Year == turnFilterDto.Date.Year)
                || (turnFilterDto.Date.Year == 2023 && turnFilterDto.Date.Month == 1 && turnFilterDto.Date.Day == 1))
                && t.IsActive == true)).Returns(turns);


            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<TurnWithDetailDto>>(turnsOnOnePage)).Returns(turnWithDetailDto);

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);
            var result = turnService.GetTurnsWithDetails(turnFilterDto);

            Assert.IsType<Tuple<IEnumerable<TurnWithDetailDto>, int>> (result);
            Assert.Equal(turns.Count(), result.Item2);
            Assert.Equal(turnWithDetailDto.Count(), result.Item1.Count());
        }
        [Fact]
        public void GetTurnsWithDetails_WhenTakeStudentId_ShouldReturnAllFilterTurns()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            TurnFilterDto turnFilterDto = new() { StudentId = id };
            var turns = new List<Turn>() { new Turn() { StudentId = id }, new Turn() { StudentId = id } }.AsQueryable();
            var turnWithDetailDto = new List<TurnWithDetailDto>() { new TurnWithDetailDto(), new TurnWithDetailDto() }.AsEnumerable();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.Find(
                t => (t.MachineId == turnFilterDto.MachineId || turnFilterDto.MachineId == Guid.Empty)
                && (t.StudentId == turnFilterDto.StudentId || turnFilterDto.StudentId == Guid.Empty)
                && ((t.Date.Day == turnFilterDto.Date.Day && (t.Date.Hour == turnFilterDto.Date.Hour + offset.Hours
                || turnFilterDto.Date.Hour <= 7)
                && t.Date.Month == turnFilterDto.Date.Month && t.Date.Year == turnFilterDto.Date.Year)
                || (turnFilterDto.Date.Year == 2023 && turnFilterDto.Date.Month == 1 && turnFilterDto.Date.Day == 1))
                && t.IsActive == true)).Returns(turns);

            var turnsOnOnePage = turns.Skip((turnFilterDto.PageNumber - 1) * 3).Take(3);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<TurnWithDetailDto>>(turnsOnOnePage)).Returns(turnWithDetailDto);

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);
            var result = turnService.GetTurnsWithDetails(turnFilterDto);

            Assert.IsType<Tuple<IEnumerable<TurnWithDetailDto>, int>>(result);
            Assert.Equal(turns.Count(), result.Item2);
            Assert.Equal(turnWithDetailDto.Count(), result.Item1.Count());
        }
        [Fact]
        public void GetTurnsWithDetails_WhenTakeDate_ShouldReturnAllFilterTurns()
        {
            DateTime date = DateTime.Now.AddDays(4);
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            TurnFilterDto turnFilterDto = new() { Date = date };
            var turns = new List<Turn>() { new Turn() { Date = date }, new Turn() { Date = date } }.AsQueryable();
            var turnWithDetailDto = new List<TurnWithDetailDto>() { new TurnWithDetailDto(), new TurnWithDetailDto() }.AsEnumerable();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.Find(
                t => (t.MachineId == turnFilterDto.MachineId || turnFilterDto.MachineId == Guid.Empty)
                && (t.StudentId == turnFilterDto.StudentId || turnFilterDto.StudentId == Guid.Empty)
                && ((t.Date.Day == turnFilterDto.Date.Day && (t.Date.Hour == turnFilterDto.Date.Hour + offset.Hours
                || turnFilterDto.Date.Hour <= 7)
                && t.Date.Month == turnFilterDto.Date.Month && t.Date.Year == turnFilterDto.Date.Year)
                || (turnFilterDto.Date.Year == 2023 && turnFilterDto.Date.Month == 1 && turnFilterDto.Date.Day == 1))
                && t.IsActive == true)).Returns(turns);

            var turnsOnOnePage = turns.Skip((turnFilterDto.PageNumber - 1) * 3).Take(3);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<IEnumerable<TurnWithDetailDto>>(turnsOnOnePage)).Returns(turnWithDetailDto);

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);
            var result = turnService.GetTurnsWithDetails(turnFilterDto);

            Assert.IsType<Tuple<IEnumerable<TurnWithDetailDto>, int>>(result);
            Assert.Equal(turns.Count(), result.Item2);
            Assert.Equal(turnWithDetailDto.Count(), result.Item1.Count());
        }
        [Fact]
        public void GetTurnsWithDetails_WhenInvalidPageNumber_ShouldThrowException()
        {
            TurnFilterDto turnFilterDto = new() { PageNumber = 0 };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            var mockMapper = new Mock<IMapper>();

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<Exception>(() => turnService.GetTurnsWithDetails(turnFilterDto));
            Assert.Equal("Lütfen geçerli bir değer giriniz.", exception.Message);
        }
        [Fact]
        public void GetTurnById_ShouldReturnTurnDto()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Turn turn = new() { Id = id, IsActive = true, Date = DateTime.UtcNow };
            TurnDto turnDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.FindAsync(x => x.Id == id && x.IsActive == true && x.Date >= DateTime.UtcNow)).ReturnsAsync(turn);
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<TurnDto>(turn)).Returns(turnDto);

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);
            var result = turnService.GetTurnByIdAsync(id).Result;

            Assert.IsType<TurnDto>(result);
            Assert.Equal(id, result.Id);

        }
        [Fact]
        public void GetTurnById_WhenTurnIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            Turn turn = new() { Id = id, IsActive = true , Date=DateTime.UtcNow};
            TurnDto turnDto = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.FindAsync(x => x.Id == id && x.IsActive == true && x.Date >= DateTime.UtcNow));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<TurnDto>(turn)).Returns(turnDto);
            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => turnService.GetTurnByIdAsync(id).Result);

            Assert.Equal("One or more errors occurred. (Sıra id'si yok!)", exception.Message);

        }
    
        [Fact]
        public void CreateTurn_WhenStudentHasTurn_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            TurnDto turnDto = new() { Id = id };
            Turn turn = new() { Id = id, IsActive = true, Date= DateTime.UtcNow };
            TurnForCreationDto turnForCreation = new();
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.FindAsync(x => x.StudentId == turn.StudentId && x.IsActive == true 
            && x.Date >= DateTime.UtcNow && x.Date <= DateTime.UtcNow.AddDays(7) || (x.Date == turn.Date && x.MachineId == turn.MachineId && x.IsActive == true))).ReturnsAsync(turn);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Turn>(turnForCreation)).Returns(turn);
            mockMapper.Setup(x => x.Map<TurnDto>(turn)).Returns(turnDto);
            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => turnService.CreateTurnAsync(turnForCreation).Result);

            Assert.Equal("One or more errors occurred. (Sırayı alamazsınız. Geçmişe dönük bir Tarih seçtiniz.)", exception.Message);
        }
        [Fact]
        public void DeleteTurn_ShouldReturnGuid()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto turnForDeleting = new() { Id = id };
            Turn turn = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.FindAsync(x => x.Id == turnForDeleting.Id && x.IsActive == true)).ReturnsAsync(turn);
            repositoryWrapper.Setup(x => x.Turn.DeleteAsync(turn));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Turn>(turnForDeleting)).Returns(turn);
            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

            var result = turnService.DeleteTurnAsync(turnForDeleting).Result;

            Assert.IsType<Guid>(result);
            Assert.Equal(id, result);

        }

        [Fact]
        public void DeleteTurn_WhenTurnIdIsNotExist_ShouldThrowException()
        {
            Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
            ForDeletingDto turnForDeleting = new() { Id = id, };
            Turn turn = new() { Id = id };
            var repositoryWrapper = new Mock<IRepositoryWrapper>();
            repositoryWrapper.Setup(x => x.Turn.FindAsync(x => x.Id == turnForDeleting.Id && x.IsActive == true));
            repositoryWrapper.Setup(x => x.Turn.DeleteAsync(turn));
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Turn>(turnForDeleting)).Returns(turn);

            var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

            var exception = Assert.Throws<AggregateException>(() => turnService.DeleteTurnAsync(turnForDeleting).Result);
            Assert.Equal("One or more errors occurred. (Sıra yok!)", exception.Message);
        }
        /*  [Fact]
      public void CreateTurn_WhenStudentHasNotTurn_ShouldReturnTurnDto()
      {
          var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
          Guid id = Guid.Parse("0f8fad5b-d9cb-469f-a165-708677289502");
          TurnDto turnDto = new() { Id = id };
          Turn turn = new() { Id = id, IsActive = true, Date = DateTime.Now.AddDays(3) };

          TurnForCreationDto turnForCreation = new();
          var repositoryWrapper = new Mock<IRepositoryWrapper>();
          repositoryWrapper.Setup(x => x.Turn.FindAsync(t => t.StudentId == turn.StudentId && t.IsActive == true));
          repositoryWrapper.Setup(x => x.Turn.FindAsync(t=> t.Date.Day == turn.Date.Day && t.Date.Hour == (turn.Date.Hour + offset.Hours)
             && t.Date.Month == turn.Date.Month && t.MachineId == turn.MachineId && t.IsActive == true));

          var mockMapper = new Mock<IMapper>();
          mockMapper.Setup(x => x.Map<Turn>(turnForCreation)).Returns(turn);
          mockMapper.Setup(x => x.Map<TurnDto>(turn)).Returns(turnDto);
          var turnService = new TurnService(repositoryWrapper.Object, mockMapper.Object);

          var result = turnService.CreateTurnAsync(turnForCreation);

          Assert.IsType<Task<TurnDto>>(result);
          Assert.Equal(id, result.Result.Id);
      }*/
    }
}