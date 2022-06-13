using Microsoft.AspNetCore.Identity;

namespace AppoitmentScheduleApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
