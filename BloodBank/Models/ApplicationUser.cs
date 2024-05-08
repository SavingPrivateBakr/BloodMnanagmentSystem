using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Index(nameof(UserName), IsUnique=false)]
    public class ApplicationUser : IdentityUser
    {

       

 
     

    }
}
