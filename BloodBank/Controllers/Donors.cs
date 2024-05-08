using AutoMapper.Configuration;
using BloodBank.Models;
using BloodBank.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace BloodBank.Controllers
{

    [Authorize(Roles ="donor nurse")]
    public class Donors : Controller
    {

        private readonly ICRUD<Blood> Blood;
        private readonly ICRUD<Donor> donor;
        private readonly ICRUD<Nurse> nurse;
        public Data d;
        public Donors(ICRUD<Blood>Blood,ICRUD<Donor>donor,Data d,ICRUD<Nurse>nurse) { 
        this.Blood=Blood;
            this.donor = donor;
            this.d=d;
            this.nurse=nurse;
        }

        [HttpGet]
        public IActionResult Donor()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Donor(Blood blood)
        {
          Donor w=  donor.GetByName(User.Identity.Name);
            Random rand = new Random();
            int skipper = rand.Next(0, nurse.GetAll(new Nurse { }));

            Nurse tt = nurse.GetAllList(new Nurse { }, skipper);

            Blood.Createblood(blood,tt.Id,w.Id) ;
           
            return View(); 
        }
      
    }
}
