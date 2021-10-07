using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Ation
{
    public interface ioperation<T>
    {
         ActionResult Add(T t);
        DataResult<T> ListAll();
        ActionResult Remove(int id);
    }
}
