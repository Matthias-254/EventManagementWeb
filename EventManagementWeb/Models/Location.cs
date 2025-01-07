using System.ComponentModel.DataAnnotations;

namespace EventManagementWeb.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Deleted")]
        public DateTime Deleted { get; set; } = DateTime.MaxValue;
    }
}
