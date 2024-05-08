using BloodBank.Models;
using BloodBank.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloodBank.Controllers
{
  
    public class Bloods : Controller
    {
        private readonly ICRUD<Blood> Blood;
        private readonly ICRUD<Donor> donor;
        private readonly ICRUD<Nurse> nurse;
        public Data d;
        public Bloods(ICRUD<Blood> Blood, ICRUD<Donor> donor, Data d, ICRUD<Nurse> nurse)
        {
            this.Blood = Blood;
            this.donor = donor;
            this.d = d;
            this.nurse = nurse;
        }

        [HttpGet]
        public IActionResult ListOfBloods()
        {
            List<Blood> list = d.Bloods.FromSqlRaw($"SELECT * FROM dbo.bloods Where Availability like '1'").ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add(Guid Id)
        {
            TempData["idd"] = Id;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Hospital hos) {

            Blood.CreateHospital(hos, (Guid)TempData["idd"]);
            return RedirectToAction("ListOfBloods");
            
        }
        [Authorize(Roles ="nurse")]
        public IActionResult Delete(Guid Id)
        {

            Blood.DeleteByID(Id);
            return RedirectToAction("ListOfBloods");

        }
    }
}
