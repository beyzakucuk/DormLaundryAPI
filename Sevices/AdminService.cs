using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Services
{
    public class AdminService : BaseService
    {
        public AdminService(IRepositoryWrapper repositoryWrapper, IMapper mapper) : base(repositoryWrapper, mapper) { }

        public async Task<IEnumerable<AdminDto>> GetAllAdminsAsync()
        {
            var adminsAsync = await repositoryWrapper.Admin.FindAllAsync();
            var admins = adminsAsync.OrderBy(a => a.Name).Where(a => a.IsActive == true);
            var adminDtos = mapper.Map<IEnumerable<AdminDto>>(admins);
            return adminDtos;
        }

        public async Task<AdminDto> GetAdminByIdAsync(Guid id)
        {
            var admin = await repositoryWrapper.Admin.FindAsync(a => a.Id == id && a.IsActive == true);
            var adminDto = mapper.Map<AdminDto>(admin);
            return adminDto ?? throw new Exception("Admin id'si yok!");
        }

        public async Task<AdminDto> CreateAdminAsync(AdminForCreationDto adminForCreation)
        {
            var admin = mapper.Map<Admin>(adminForCreation);
            admin.IsActive = true;
            admin.CreationDate = DateTime.UtcNow;
            admin.CreaterId = adminForCreation.CreaterId;
            await repositoryWrapper.Admin.CreateAsync(admin);
            var adminDto = mapper.Map<AdminDto>(admin);
            return adminDto;
        }

        public async Task<Guid> DeleteAdminAsync(ForDeletingDto adminForDeletingDto)
        {
            var admin = await repositoryWrapper.Admin.FindAsync(a => a.Id == adminForDeletingDto.Id && a.IsActive == true);
            if (admin != null)
            {
                admin.IsActive = false;
                admin.DeletedDate = DateTime.UtcNow;
                admin.DeletoryId = adminForDeletingDto.DeletoryId;
                await repositoryWrapper.Admin.DeleteAsync(admin);
                return adminForDeletingDto.Id;

            }
            else
            {
                throw new Exception("Admin yok!");
            }
        }


    }
}
