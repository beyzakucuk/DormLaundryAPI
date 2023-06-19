using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services
{
    public class StudentService : BaseService
    {
        public StudentService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper) { }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var studentsAsync =await repositoryWrapper.Student.FindAllAsync();
            var students = studentsAsync.OrderBy(s => s.Name).Where(s => s.IsActive == true);
            var studentDtos = mapper.Map<IEnumerable<StudentDto>>(students);
            return studentDtos;
        }

        public async Task<StudentDto> GetStudentByIdAsync(Guid studentId)
        {
            var student = await repositoryWrapper.Student.FindAsync(s => s.Id == studentId && s.IsActive == true);
            var studentDto = mapper.Map<StudentDto>(student);
            return studentDto ?? throw new Exception("Öğrenci id'si yok!");
        }

        public async Task<StudentDto> CreateStudentAsync(StudentForCreationDto studentForCreation)
        {
            var student = mapper.Map<Student>(studentForCreation);
            student.IsActive = true;
            student.CreationDate = DateTime.UtcNow;
            student.CreaterId = studentForCreation.CreaterId;
            await repositoryWrapper.Student.CreateAsync(student);
            var studentDto = mapper.Map<StudentDto>(student);
            return studentDto;
        }

        public async Task<Guid> DeleteStudentAsync(ForDeletingDto studentForDeletingDto)
        {
            var student =await repositoryWrapper.Student.FindAsync(a => a.Id == studentForDeletingDto.Id && a.IsActive == true);
            if (student != null)
            {
                student.IsActive = false;
                student.DeletedDate = DateTime.UtcNow;
                student.DeletoryId = student.Id;
                student.DeletoryId = studentForDeletingDto.DeletoryId;
                await repositoryWrapper.Student.DeleteAsync(student);
                return studentForDeletingDto.Id;
            }
            else
            {
                throw new Exception("Öğrenci yok!");
            }
        }
    }
}
