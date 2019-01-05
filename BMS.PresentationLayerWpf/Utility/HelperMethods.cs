using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.PresentationLayerWpf.Utility
{
    public class HelperMethods
    {
        public static string SHA256(string messageText)
        {
            System.Security.Cryptography.SHA256Managed cryptography = new System.Security.Cryptography.SHA256Managed();
            StringBuilder hashCode = new StringBuilder();
            byte[] codes = cryptography.ComputeHash(Encoding.UTF8.GetBytes(messageText));
            foreach (var item in codes)
            {
                hashCode.Append(item.ToString("x2"));
            }
            return hashCode.ToString();
        }

        public static string UniqueSystemId()
        {
            string uniqueText = Environment.ProcessorCount + "/" +
                                Environment.MachineName + "/" +
                                Environment.UserDomainName + "\\" +
                                Environment.UserName;
            return SHA256(uniqueText);
        }

        public static string SystemId()
        {
            return SHA256(UniqueSystemId());
        }
    }
}
