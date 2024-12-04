using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementWeb.Models
{
    public class EventStaff
    {
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; } = 1;

        [Display(Name = "Staff")]
        [ForeignKey("EventManagementUser")]
        public string StaffId { get; set; } = "?";

        [Display(Name = "Became member")]
        public DateTime BecameMemberDate { get; set; } = DateTime.Now;

        [Display(Name = "Added by")]
        public string DoneById { get; set; } = "?";     // Person who added the staff to the event.

        [Display(Name = "Remark")]
        public string Remark { get; set; } = "";
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        [Display(Name = "Event")]
        public Event? Event { get; set; }

        [Display(Name = "Staff")]
        public EventManagementUser? Staff { get; set; }
    }
}
