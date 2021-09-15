using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Ation;

namespace Domain
{
    public class Employeemanager
    {
        private static List<Employee> _employeeList;
        static Employeemanager()
        {
            _employeeList = new List<Employee>();
        }
        public ActionResult AddEmployee(Employee emp)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
               _employeeList.Add(emp);
                result.status = "Employee added";
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured" + ex.ToString());
                result.isSucess = false;
            }
            return result;
        }

        public DataResult<Employee> GetEmployeeInfo()
        {
            DataResult<Employee> data = new DataResult<Employee> { isSucess = true };
            if (_employeeList.Count > 0)
            {
                data.Results = _employeeList;
            }
            else
            {
                data.isSucess = false;
                data.status = "List is Empty!";
            }
            return data;
        }

        
    }
}




