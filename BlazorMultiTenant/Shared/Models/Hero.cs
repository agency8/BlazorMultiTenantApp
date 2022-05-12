using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Shared.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string TenantId { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(128)")]
        [Required(ErrorMessage = "The first name of the super hero is required!")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(128)")]
        [Required(ErrorMessage = "The surname of the super hero is required!")]
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(128)")]
        [Required(ErrorMessage = "The hero name of the super hero is required!")]
        public string HeroName { get; set; } = string.Empty;

        public Comic? Comic { get; set; }
        public int ComicId { get; set; }
    }
}
