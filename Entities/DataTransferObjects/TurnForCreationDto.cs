using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class TurnForCreationDto
    {
        // [Range(typeof(DateTime), "01/03/2023", "30/03/2023", ErrorMessage = "{0} değeri {1} ve {2} arasında olmalı")]
        [Required(ErrorMessage = "Tarih zorunlu alandır")]
        [DisplayFormat(DataFormatString = "{MM/dd/yy H}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Öğrenci id'si zorunlu alandır")]
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "Makine id'si zorunlu alandır")]
        public Guid MachineId { get; set; }
    }
}
