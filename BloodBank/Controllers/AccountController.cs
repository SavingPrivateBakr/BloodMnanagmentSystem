using AutoMapper;
using BloodBank.Mapping;
using BloodBank.Models;
using BloodBank.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;

namespace BloodBank.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly ICRUD<Nurse> nurse;
        private readonly ICRUD<Donor> donor;
        private readonly ICRUD<ApplicationUser> applicationUser;
        private readonly Data d;
        public AccountController(IMapper mapper,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> RoleManager, ICRUD<Nurse> nurse,ICRUD<Donor> donor, Data d)
        {
            this.userManager = userManager;
        this.mapper = mapper;
            this.signInManager = signInManager;
            this.RoleManager = RoleManager;
            this.nurse=nurse;
            this.donor=donor;
            this.d = d;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto app)
        {


            if (ModelState.IsValid)
            {
                if (d.Users.Any(w => w.PhoneNumber == app.PhoneNumber) == true)
                {
                    ModelState.AddModelError("PhoneNumber", "PhoneNumber Duplicated");
                    return View();
                }
                

                ApplicationUser ww = mapper.Map<ApplicationUser>(app);

               
                IdentityResult re = await userManager.CreateAsync(ww, app.Password);
                

                
                if (re.Succeeded)
                {

                    await userManager.AddToRolesAsync(ww, roles: ["Donor"]);


                    Donor www = mapper.Map<Donor>(app);
                    Random rand = new Random();

                    int skipper = rand.Next(0, nurse.GetAll(new Nurse { }));

                
                    
                  Nurse tt= nurse.GetAllList(new Nurse { },skipper);
                   
                    www.Nurses = tt;
                   
                   www.Id = Guid.Parse(ww.Id);
                
                    donor.Createdonor(www,tt.Id);

                    await signInManager.SignInAsync(ww, true);
              

                    return RedirectToAction("Index", "Account");

                }
                else
                {
                    foreach (var i in re.Errors)
                    {
                        ModelState.AddModelError("", i.Description);
                    }
                }
            }


            return View();

        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO login)
        {

            
            ApplicationUser? result = await userManager.FindByEmailAsync(login.Email);

            if (result != null)
            {
                bool re = await userManager.CheckPasswordAsync(result, login.Password);
                if (re == true)
                {
                    await signInManager.SignInAsync(result, true);
                    
                    return RedirectToAction("Donor", "Donors");
                }
            }
            else
            {
                ModelState.AddModelError("", "Something Wrong");
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }
    }
}
