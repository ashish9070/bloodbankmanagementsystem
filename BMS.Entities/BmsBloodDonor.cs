using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entities
{
    public class BmsBloodDonor
    {
        public BmsBloodDonor()
        {
            //this.BmsBloodDonorDonation = new HashSet<BmsBloodDonorDonation>();
        }

        public int BloodDonorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNo { get; set; }
        public string BloodGroup { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
