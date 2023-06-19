using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Linq.Expressions;

namespace Services
{
    public class TurnService : BaseService
    {
        public TurnService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper) { }

        public async Task<IEnumerable<TurnDto>> GetAllTurnsAsync()
        {

            var turnsAsync = await repositoryWrapper.Turn.FindAllAsync();
            var turns = turnsAsync.OrderBy(t => t.Date)
                .Where(t => t.IsActive == true && t.Date >= DateTime.UtcNow && t.Date <= DateTime.UtcNow.AddDays(7));

            var turnDtos = mapper.Map<IEnumerable<TurnDto>>(turns);
            return turnDtos;
        }
        public Tuple<IEnumerable<TurnDto>, int> GetTurnsWithDetails(TurnFilterDto turnFilterDto)
        {
            if (turnFilterDto.PageNumber <= 0)
                throw new Exception("Lütfen geçerli bir değer giriniz.");

            TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);

            var turns = repositoryWrapper.Turn.Find(
             t => (t.MachineId == turnFilterDto.MachineId || turnFilterDto.MachineId == Guid.Empty)
             && (t.StudentId == turnFilterDto.StudentId || turnFilterDto.StudentId == Guid.Empty)
             && ((t.Date.Day == turnFilterDto.Date.Day && (t.Date.Hour == turnFilterDto.Date.Hour + offset.Hours
             || turnFilterDto.Date.Hour <= (7 - offset.Hours))
             && t.Date.Month == turnFilterDto.Date.Month && t.Date.Year == turnFilterDto.Date.Year)
             || (turnFilterDto.Date.Year == 2023 && turnFilterDto.Date.Month == 1 && turnFilterDto.Date.Day == 1))
             && t.IsActive == true)
            .Include(turn => turn.Student).Include(turn => turn.Machine).OrderBy(t => t.Date);
            var turnsInOnePage = turns.Skip((turnFilterDto.PageNumber - 1) * 3).Take(3);


            var turnDtos = mapper.Map<IEnumerable<TurnDto>>(turnsInOnePage);
            return Tuple.Create(turnDtos, turns.Count());
        }

        public async Task<TurnDto> GetTurnByIdAsync(Guid id)
        {
            var turn = await repositoryWrapper.Turn.FindAsync(t => t.Id == id && t.IsActive == true && t.Date >= DateTime.UtcNow);
            var turnDto = mapper.Map<TurnDto>(turn);
            return turnDto ?? throw new Exception("Sıra id'si yok!");
        }

        public async Task<TurnDto> CreateTurnAsync(TurnForCreationDto turnForCreationDto)
        {
            if (turnForCreationDto.Date <= DateTime.UtcNow)
                throw new Exception("Sırayı alamazsınız. Geçmişe dönük bir Tarih seçtiniz.");

            if (turnForCreationDto.Date >= DateTime.UtcNow.AddDays(7))
                throw new Exception("Sırayı alamazsınız. Lütfen önümüzdeki 1 hafta içinde tarih seçiniz.");

            if (Convert.ToInt32(turnForCreationDto.Date.Hour) == 21)
                throw new Exception("Sırayı alamazsınız. Lütfen geçerli bir saat seçiniz");

            var turn = mapper.Map<Turn>(turnForCreationDto);
            if (await repositoryWrapper.Student.FindAsync(s => s.Id == turnForCreationDto.StudentId) == default)
                throw new Exception("Sıra alamazsınız. Öğrenci sistemde bulunmamaktadır.");

            var turnStudentExist = await repositoryWrapper.Turn
                .FindAsync(t => (t.StudentId == turn.StudentId && t.IsActive == true));

            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            var turnDateExist = await repositoryWrapper.Turn
               .FindAsync(t => (t.Date.Day == turn.Date.Day && t.Date.Hour == (turn.Date.Hour + offset.Hours)
               && t.Date.Month == turn.Date.Month && t.MachineId == turn.MachineId && t.IsActive == true));

            if (turnDateExist != default)
                throw new Exception("Sıra dolu alamazsınız");
            if (turnStudentExist != default)
                throw new Exception("Sıranız mevcut, yeni sıra alamazsınız");
            else
            {
                turn.IsActive = true;
                turn.CreationDate = DateTime.UtcNow;
                turn.CreaterId = turnForCreationDto.StudentId;
                await repositoryWrapper.Turn.CreateAsync(turn);
                var turnDto = mapper.Map<TurnDto>(turn);
                return turnDto;
            }
        }
        public async Task<Guid> DeleteTurnAsync(ForDeletingDto turnForDeletingDto)
        {

            var turn = await repositoryWrapper.Turn.FindAsync(t => t.Id == turnForDeletingDto.Id && t.IsActive == true);
            if (turn != default)
            {
                turn.IsActive = false;
                turn.DeletedDate = DateTime.UtcNow;
                turn.DeletoryId = turnForDeletingDto.DeletoryId;
                await repositoryWrapper.Turn.DeleteAsync(turn);
                return turnForDeletingDto.Id;
            }
            else
            {
                throw new Exception("Sıra yok!");
            }
        }
    }
}
