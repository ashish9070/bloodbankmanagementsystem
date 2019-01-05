using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entities
{
    public partial class BmsBloodDonorDonation
    {
        public int BloodDonationID { get; set; }
        public int BloodDonorID { get; set; }
        public DateTime BloodDonationDate { get; set; }
        public int NumberofBottle { get; set; }
        public decimal Weight { get; set; }
        public decimal HBCount { get; set; }
        public int BloodDonationCampID { get; set; }
        public int BloodInventoryID { get; set; }

    }
}
