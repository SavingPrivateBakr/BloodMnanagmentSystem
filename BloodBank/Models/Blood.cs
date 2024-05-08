using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BloodBank.Models
{
    public class Blood
    {
        public Guid Id { get; set; }

        public string BloodType { get; set;}

        public char Availability { get; set;
                }

        public Donor Donors { get; set; }

        public  Nurse Nurses { get; set; }


        [AllowNull]
        public Hospital Hospital { get; set; }
        
    }
}
