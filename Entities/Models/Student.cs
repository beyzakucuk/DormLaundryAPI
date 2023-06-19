using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("students")]
    public class Student:BaseEntity
    {
        public int Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public int RoomNo { get; set; }
    }
}
