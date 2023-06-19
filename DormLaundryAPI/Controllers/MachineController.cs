using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DormLaundryAPI.Controllers
{
    [Route("api/machine")]
    [ApiController]
    public class MachineController : Controller
    {
        private readonly MachineService service;

        public MachineController(MachineService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMachines()
        {
            try
            {
                var machines = await service.GetAllMachinesAsync();
                return Ok(machines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineById(Guid id)
        {
            try
            {
                var machine = await service.GetMachineByIdAsync(id);
                return Ok(machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMachine([FromBody] MachineForCreationDto machineForCreation)
        {
            try
            {
                var machine = await service.CreateMachineAsync(machineForCreation);
                return CreatedAtRoute(new { id = machine.Id }, machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMachine([FromBody]ForDeletingDto machineForDeletingDto)
        {
            try
            {
                var machine= await service.DeleteMachineAsync(machineForDeletingDto);
                return Ok(machine);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

