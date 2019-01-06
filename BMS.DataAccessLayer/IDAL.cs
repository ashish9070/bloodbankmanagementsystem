using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.DataAccessLayer
{
    public interface IDAL<T>
    {
        bool Insert(T value);
        bool Delete(T value);
        bool Update(T value);
        List<T> SelectAll();
    }
}
