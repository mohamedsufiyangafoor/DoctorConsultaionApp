using AppoitmentScheduleApp.Models;
using AppoitmentScheduleApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace AppoitmentScheduleApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController( ApplicationDbContext db,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }



        public IActionResult Login()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel LoginData)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginData.Email, LoginData.Password, LoginData.RememberMe,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Appointment");
                }
                ModelState.AddModelError("", "Invalid Login attempt");
            }

            return View(LoginData);
        }



        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(Helper.Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Doctor));
                await _roleManager.CreateAsync(new IdentityRole(Helper.Helper.Patient));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel RegisterData)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = RegisterData.Email,
                    Email = RegisterData.Email,
                    Name = RegisterData.Name,
                    
                };

                var result = await _userManager.CreateAsync(user,RegisterData.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RegisterData.RoleName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(RegisterData);
        }
        
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
