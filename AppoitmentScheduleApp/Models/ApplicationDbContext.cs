using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppoitmentScheduleApp.Models.ViewModels;

namespace AppoitmentScheduleApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        //public DbSet<AppoitmentScheduleApp.Models.ViewModels.DoctorVM> DoctorVM { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
