using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Entities.DataTransferObjects
{
    public class ForDeletingDto
    {
        [Required(ErrorMessage = "Id zorunlu alandır")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Silen id'si zorunlu alandır")]
        public Guid DeletoryId { get; set; }
    }
}
