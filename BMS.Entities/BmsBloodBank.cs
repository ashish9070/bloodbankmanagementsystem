using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Entities
{
    public class BmsBloodBank
    {
        public BmsBloodBank()
        {
            
        }

        public int BloodBankID { get; set; }
        public string BloodBankName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string SysId { get; set; }
        public DateTime CreationDate { get; set; }
        
    }
}
