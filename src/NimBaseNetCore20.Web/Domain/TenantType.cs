using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NimBaseNetCore20.Domain
{
    public class TenantType : AuditData
    {
        [Key]
        public int TenantTypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Tenant> Tenants { get; set; }
    }
}
