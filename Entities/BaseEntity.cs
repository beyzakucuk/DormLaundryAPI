using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public abstract class BaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }

        public Guid? CreaterId { get; set; }

        public Guid? DeletoryId { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool? IsActive { get; set; }

    }
}
