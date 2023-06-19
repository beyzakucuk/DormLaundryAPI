using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Providers
{
    public class TurnServiceProvider: ControllerBase
    {
        private readonly IServicesWrapper _service;
        private readonly IMapper _mapper;

        public TurnServiceProvider(IServicesWrapper service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IActionResult GetAllTurns()
        {
            try
            {
                var turns = _service.Turn.GetAllTurns();
                return Ok(turns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult GetTurnById(Guid id)
        {
            try
            {
                var turn = _service.Turn.GetTurnById(id);
                return Ok(turn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult CreateTurn([FromBody] TurnForCreationDto turnForCreation)
        {
            try
            {
                var turn= _service.Turn.CreateTurn(turnForCreation);
                return CreatedAtRoute(new { id = turn.Id }, turn.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult DeleteTurn(Guid id)
        {
            try
            {
                var student = _service.Turn.DeleteTurn(id);
                return CreatedAtRoute(new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
