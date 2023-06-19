using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class TurnDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid MachineId { get; set; }

        public Guid StudentId { get; set; }
    }
}
