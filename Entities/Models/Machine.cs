using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    [Table("machines")]
    public class Machine:BaseEntity
    {
        public int No { get; set; }
    }
}
