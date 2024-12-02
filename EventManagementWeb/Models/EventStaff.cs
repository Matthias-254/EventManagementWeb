using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementWeb.Models
{
    public class EventStaff
    {
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; } = 1;

        [ForeignKey("EventManagementUser")]
        public string StaffId { get; set; } = "?";

        public DateTime BecameMemberDate { get; set; } = DateTime.Now;

        public string DoneById { get; set; } = "?";     // Person who added the staff to the event.

        public string Remark { get; set; } = "";
        public DateTime Deleted { get; set; } = DateTime.MaxValue;

        public Event? Event { get; set; }
        public EventManagementUser? Staff { get; set; }
    }
}
