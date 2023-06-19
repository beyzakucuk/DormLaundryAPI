using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class AdminRepository: BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

       
    }
}
