using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace DormLaundryAPI.Controllers
{
    [Route("api/turn")]
    [ApiController]
    public class TurnController : ControllerBase
    {
        private readonly TurnService service;

        public TurnController(TurnService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTurns()
        {
            try
            {
                IEnumerable<TurnDto> turns = await service.GetAllTurnsAsync();
                return Ok(turns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("details/")]
        public IActionResult GetTurnsWithDetails([FromQuery] TurnFilterDto turnFilterDto)
        {
            try
            {
                var turns =  service.GetTurnsWithDetails(turnFilterDto);
                return Ok(turns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTurnById(Guid id)
        {
            try
            {
                TurnDto turn = await service.GetTurnByIdAsync(id);
                return Ok(turn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurn([FromBody] TurnForCreationDto turnForCreation)
        {
            try
            {
                TurnDto turn = await service.CreateTurnAsync(turnForCreation);
                return CreatedAtRoute(new { id = turn.Id }, turn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTurn([FromBody] ForDeletingDto turnForDeletingDto)
        {
            try
            {
                var turn = await service.DeleteTurnAsync(turnForDeletingDto);
                return Ok(turn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}
