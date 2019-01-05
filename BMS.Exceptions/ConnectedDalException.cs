using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Exceptions
{
    [Serializable]
    public class ConnectedDalException : Exception
    {
        public ConnectedDalException() { }
        public ConnectedDalException(string message) : base(message) { }
        public ConnectedDalException(string message, Exception inner) : base(message, inner) { }
        protected ConnectedDalException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
