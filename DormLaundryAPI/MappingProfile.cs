using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace DormLaundryAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Turn, TurnDto>();
            CreateMap<TurnDto, Turn>();
            CreateMap<TurnWithDetailDto, Turn>();
            CreateMap<Turn, TurnWithDetailDto>();
            CreateMap<TurnForCreationDto, Turn>();
            CreateMap<StudentDto, Student>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentForCreationDto, Student>();
            CreateMap<MachineDto, Machine>();
            CreateMap<Machine, MachineDto>();
            CreateMap<MachineForCreationDto, Machine>();
            CreateMap<AdminDto, Admin>();
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminForCreationDto, Admin>();
            CreateMap<ForDeletingDto, Admin>();
            CreateMap<ForDeletingDto, Student>();
            CreateMap<ForDeletingDto, Machine>();
            CreateMap<ForDeletingDto, Turn>();



        }

    }
}
