using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Entities.DataTransferObjects
{
    public class StudentForCreationDto 
    {
        [Required(ErrorMessage = "Şifre zorunlu alandır")]
        public int Password { get; set; }

        [MinLength(3, ErrorMessage = "İsim en az 3 karakter olmalı.")]
        [MaxLength(25, ErrorMessage = "İsim en fazla 25 karakter olmalı.")]
        public string? Name { get; set; }

        [MinLength(2, ErrorMessage = "Soyisim en az 2 karakter olmalı.")]
        [MaxLength(25, ErrorMessage = "Soyisim en fazla 30 karakter olmalı.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Oda numarası zorunlu alandır")]
        public int RoomNo { get; set; }

        [Required(ErrorMessage = "Oluşturan id'si zorunlu alandır")]
        public Guid CreaterId { get; set; }
    }
}
