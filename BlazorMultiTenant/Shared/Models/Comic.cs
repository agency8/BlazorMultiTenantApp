using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorMultiTenant.Shared.Models
{
    public class Comic
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(128)")]
        [Required(ErrorMessage = "The name of the comic is required!")]
        public string Name { get; set; } = string.Empty;
    }
}
