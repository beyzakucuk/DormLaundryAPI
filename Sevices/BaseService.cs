using AutoMapper;
using Contracts;

namespace Services
{
    public abstract class BaseService
    {
        protected readonly IRepositoryWrapper repositoryWrapper;
        protected readonly IMapper mapper;

        protected BaseService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            this.repositoryWrapper = repositoryWrapper;
            this.mapper = mapper;
        }
    }
}
