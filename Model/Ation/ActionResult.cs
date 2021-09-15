using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Ation
{
   public class ActionResult
    {
        public bool isSucess { get; set; }
        public string status { get; set; }
    }
    public class DataResult<T>: ActionResult
    {
        public IEnumerable<T> Results;
    }
}
