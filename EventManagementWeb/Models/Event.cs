using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementWeb.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Started")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Will end")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        [ForeignKey("Location")]
        public int LocationId { get; set; } = 1;

        [ForeignKey("EventManagementUser")]
        public string? StartedById { get; set; } = "?";

        [Display(Name = "Started by")]
        public EventManagementUser? StartedBy { get; set; }

        public DateTime Deleted { get; set; } = DateTime.MaxValue;
        public Location? Location { get; set; }
    }
}
