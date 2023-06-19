using Entities.DataTransferObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("turns")]
    public class Turn:BaseEntity
    {
        [ForeignKey(nameof(Machine))]
        public Guid MachineId { get; set; }

        public Machine? Machine { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
