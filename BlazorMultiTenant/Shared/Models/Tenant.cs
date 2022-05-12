using BlazorMultiTenant.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Shared.Models
{
    public class Tenant
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(128)")]
        public string TenantName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(50)")]
        public TenantTypes TenantType { get; set; } = new TenantTypes();

        public string DBConnectionString { get; set; } = string.Empty;
    }
}
