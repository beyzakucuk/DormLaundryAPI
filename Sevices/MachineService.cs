using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services
{
    public class MachineService : BaseService
    {
        public MachineService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper) { }

        public async Task<IEnumerable<MachineDto>> GetAllMachinesAsync()
        {
            var machinesAsync = await repositoryWrapper.Machine.FindAllAsync();
            var machines = machinesAsync.OrderBy(t => t.No).Where(m => m.IsActive == true);
            var machineDtos = mapper.Map<IEnumerable<MachineDto>>(machines);
            return machineDtos;
        }

        public async Task<MachineDto> GetMachineByIdAsync(Guid id)
        {
            var machine = await repositoryWrapper.Machine.FindAsync(m => m.Id == id && m.IsActive == true);
            var machineDto = mapper.Map<MachineDto>(machine);
            return machineDto ?? throw new Exception("Makine id'si yok!");
        }
        public async Task<MachineDto> CreateMachineAsync(MachineForCreationDto machineForCreation)
        {
            var machine = mapper.Map<Machine>(machineForCreation);
            if ( await repositoryWrapper.Machine.FindAsync(m => m.No == machine.No)!=default)
                throw new Exception("Bu numaraya sahip makine mevcut");

            machine.IsActive = true;
            machine.CreationDate = DateTime.UtcNow;
            machine.CreaterId = machineForCreation.CreaterId;
            await repositoryWrapper.Machine.CreateAsync(machine);
            var machineDto = mapper.Map<MachineDto>(machine);
            return machineDto;
        }

        public async Task<Guid> DeleteMachineAsync(ForDeletingDto machineForDeletingDto)
        {
            var turn = await repositoryWrapper.Turn.FindAsync(t => t.MachineId == machineForDeletingDto.Id && t.IsActive == true);
            if (turn!=default)
                throw new Exception(turn.Id+" id'li sıra bu makine üzerinedir. Silemezsiniz!");

            var machine =await repositoryWrapper.Machine.FindAsync(t => t.Id == machineForDeletingDto.Id && t.IsActive == true);
            if (machine != null)
            {
                machine.IsActive = false;
                machine.DeletedDate = DateTime.UtcNow;
                machine.DeletoryId = machineForDeletingDto.DeletoryId;
                await repositoryWrapper.Machine.DeleteAsync(machine);
                return machineForDeletingDto.Id;
            }
            else
            {
                throw new Exception("Makine yok!");
            }
        }


    }
}
