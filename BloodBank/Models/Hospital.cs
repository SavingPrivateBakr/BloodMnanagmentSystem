using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class Hospital
    {
        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]$")]
        public string Name { get; set; }
       
        public string address { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [MinLength(11, ErrorMessage = "You're missing somenumbers")]
        [MaxLength(11, ErrorMessage = "That's too much")]
        public string PhoneNumber { get; set; }

        public Blood Blood {  get; set; }


    }
}
