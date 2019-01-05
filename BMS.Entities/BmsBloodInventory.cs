using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entities
{
    public class BmsBloodInventory
    {
        public int BloodInventoryID { get; set; }
        public string BloodGroup { get; set; }
        public int NumberofBottles { get; set; }
        public int BloodBankID { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }

        //public virtual BmsBloodBank BmsBloodBank { get; set; }
    }
}
