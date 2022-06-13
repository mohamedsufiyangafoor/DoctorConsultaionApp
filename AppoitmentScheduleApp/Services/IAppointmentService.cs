using AppoitmentScheduleApp.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppoitmentScheduleApp.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorList();
        public List<PatientVM> GetPatientList();
        public Task<int> AddUpdate(AppointmentVM appointmentData);

    }
}
