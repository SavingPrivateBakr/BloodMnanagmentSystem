using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class Donor
    {
        
        public Guid Id { get; set;}
       
        public string Name { get; set;}

        public string Address { get; set;}

        public string Gender { get; set;}

        public bool Diabetes { get; set;}

        public bool BloodPressure { get; set;}

        [MinLength(11, ErrorMessage = "You're missing somenumbers")]
        [MaxLength(11,ErrorMessage ="That's too much")]
        public string PhoneNumber { get; set;}

     
        public string Age { get; set;}

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
     
        public string Email { get; set; }
        public List<Blood> Bloods { get; set;}

        public Nurse Nurses { get; set;}
    }
}
