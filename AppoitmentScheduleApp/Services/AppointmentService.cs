using AppoitmentScheduleApp.Models;
using AppoitmentScheduleApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoitmentScheduleApp.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return doctors;
        }


        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                            join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                            join roles in _db.Roles.Where(x => x.Name == Helper.Helper.Patient) on userRoles.RoleId equals roles.Id
                            select new PatientVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                           ).ToList();
            return patients;
        }
        public async Task<int> AddUpdate(AppointmentVM appointmentData)
        {
            var startDate = DateTime.Parse(appointmentData.StartDate);
            var endDate = DateTime.Parse(appointmentData.StartDate).AddMinutes(Convert.ToDouble(appointmentData.Duration));

            if( appointmentData != null && appointmentData.Id > 0 )
            {
                // UPDATE
                return 1;
            }
            else
            {
                // CREATE
                Appointment appointment = new Appointment()
                {
                    Title = appointmentData.Title,
                    Description = appointmentData.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = appointmentData.Duration,
                    DoctorId = appointmentData.DoctorId,
                    PatientId = appointmentData.PatientId,
                    IsDoctorApproved = false,
                    AdminId = appointmentData.AdminId
                };

                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }


    }
}
