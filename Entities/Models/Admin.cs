using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("admins")]

    public class Admin:BaseEntity 
    {
        public int Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }
    }
}
