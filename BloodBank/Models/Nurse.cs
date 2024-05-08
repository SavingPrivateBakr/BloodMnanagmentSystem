using System.ComponentModel.DataAnnotations;

namespace BloodBank.Models
{
    public class Nurse
    {
        public Guid Id { get; set; }
        [RegularExpression("^[a-zA-Z_ ]*$")]
        public string Name { get; set; }

        public string Address { get; set; }
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        public string Gender { get; set; }

        [MinLength(11, ErrorMessage = "You're missing somenumbers")]
        [MaxLength(11, ErrorMessage = "That's too much")]
        public string Phone { get; set; }

        [RegularExpression("", ErrorMessage = "Data Not Supported")]
        public string Age { get; set; }

        public List<Blood> Bloods { get; set; }

        public List<Donor> Donors { get; set; }


    }
}
