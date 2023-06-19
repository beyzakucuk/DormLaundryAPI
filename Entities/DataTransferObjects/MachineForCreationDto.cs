using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Entities.DataTransferObjects
{
    public class MachineForCreationDto
    {
        [Required(ErrorMessage = "Makine no zorunlu alandır")]
        public int No { get; set; }

        [Required(ErrorMessage = "Oluşturan id'si zorunlu alandır")]
        public Guid CreaterId { get; set; }

    }
}
