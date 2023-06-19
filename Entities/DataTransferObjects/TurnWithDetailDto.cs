
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public partial class TurnWithDetailDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Hour { get; set; }
        public int MachineNo { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public int StudentRoomNo { get; set; }
    }
}
