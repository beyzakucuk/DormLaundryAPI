using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TurnFilterDto
    {
        public Guid MachineId { get; set; }
        public Guid StudentId { get; set; }
        public DateTime Date { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
