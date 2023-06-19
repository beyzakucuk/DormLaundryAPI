using Contracts;
using Entities;

namespace Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext repoContext;
        private ITurnRepository? turn;
        private IStudentRepository? student;
        private IMachineRepository? machine;
        private IAdminRepository? admin;
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            repoContext = repositoryContext;
        }
        public ITurnRepository Turn
        {
            get
            {
                turn ??= new TurnRepository(repoContext);
                return turn;
            }
        }
        public IStudentRepository Student
        {
            get
            {
                student ??= new StudentRepository(repoContext);
                return student;
            }
        }
        public IMachineRepository Machine
        {
            get
            {
                machine ??= new MachineRepository(repoContext);
                return machine;
            }
        }
        public IAdminRepository Admin
        {
            get
            {
                admin ??= new AdminRepository(repoContext);
                return admin;
            }
        }
    }
}
