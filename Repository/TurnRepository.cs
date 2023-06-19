using Contracts;
using Entities;
using Entities.Models;

namespace Repositories
{
    public class TurnRepository : BaseRepository<Turn>, ITurnRepository
    {
        public TurnRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

    }
}
