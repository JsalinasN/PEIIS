using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Domain
{
    public class Tenant : AuditData
    {
        [Key]
        public Guid TenantId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public virtual TenantType TenantType { get; set; }
    }
}
