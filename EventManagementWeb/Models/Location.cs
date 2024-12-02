using System.ComponentModel.DataAnnotations;

namespace EventManagementWeb.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public DateTime Deleted { get; set; } = DateTime.MaxValue;
    }
}
