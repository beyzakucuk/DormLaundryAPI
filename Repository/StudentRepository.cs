using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    }
}
