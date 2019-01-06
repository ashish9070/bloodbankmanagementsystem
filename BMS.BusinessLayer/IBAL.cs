using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BusinessLayer
{
    public interface IBAL<T>
    {
        bool Add(T value);
        bool Remove(T value);
        bool Modify(T value);
        List<T> GetAll();
    }
}
