using AppoitmentScheduleApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppoitmentScheduleApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            ViewBag.DoctorList = _appointmentService.GetDoctorList();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            ViewBag.Duration = Helper.Helper.GetTimeDropDown();
            return View();
            
        }
    }
}
