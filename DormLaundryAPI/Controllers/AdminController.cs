using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DormLaundryAPI.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService service;

        public AdminController(AdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            try
            {
                var admins = await service.GetAllAdminsAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(Guid id)
        {
            try
            {
                var admin = await service.GetAdminByIdAsync(id);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody] AdminForCreationDto adminForCreation)
        {
            try
            {
                var admin = await service.CreateAdminAsync(adminForCreation);
                return CreatedAtRoute(new { id = admin.Id }, admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTurn([FromBody] ForDeletingDto adminForDeletingDto)
        {
            try
            {
                var admin = await service.DeleteAdminAsync(adminForDeletingDto);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

