using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Domain
{
    public class AuditData
    {
        public virtual int Status { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public DateTime ChangeDate { get; set; }

        [Required]
        [MaxLength(256)]
        public string CreationUser { get; set; }

        [MaxLength(256)]
        public string ChangeUser { get; set; }
    }
}
