using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entities
{
    public class BmsTransaction
    {
        public int TransactionID { get; set; }
        public int HospitalID { get; set; }
        public int BloodInventoryID { get; set; }
        public int NumberofBottles { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
