using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class MachineRepository : BaseRepository<Machine>, IMachineRepository
    {
        public MachineRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    }
}
