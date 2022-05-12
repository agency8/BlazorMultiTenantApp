using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(128)")]
        public string TenantId { get; set; } = string.Empty;
    }
}
