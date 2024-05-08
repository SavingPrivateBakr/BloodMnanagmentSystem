using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BloodBank.Mapping
{
   
    public class RegisterDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public bool Diabetes { get; set; }

        public bool BloodPressure { get; set; }

        [MinLength(11, ErrorMessage = "You're missing somenumbers")]
        [MaxLength(11, ErrorMessage = "That's too much")]
      
        [DataType(DataType.PhoneNumber)]
       
        public string PhoneNumber { get; set; }

        public string Age { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        public string Password { get; set; }
        [Compare("Password")]
        public string confirmPassword { get; set; }

    }
}
