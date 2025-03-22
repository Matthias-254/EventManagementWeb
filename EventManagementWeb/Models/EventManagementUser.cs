using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementWeb.Models
{
    public class EventManagementUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(2)]
        [ForeignKey("Languages")]
        public string LanguageCode { get; set; } = "?";

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
