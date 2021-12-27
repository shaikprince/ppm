using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

using Newtonsoft.Json.Linq;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IActionResult AddEmployee(Employee Emp)
        {
            if (Emp == null)
            {
                return NotFound();
            }
            Employeemanager employeemanager = new();
            var addEmployee = Employeemanager.Add(Emp);
            if (addEmployee.isSucess)
            {
                return Ok();
            }
            return ValidationProblem();
        }
        //public DataResult<Employee> ListAll()
        // {
        // if (EmployeeList.Count > 0)
        // {
       //   return data();
       //   }
      // DataResult<Employee> data = new();
       //var ListEmployee = Employee.List(employee);
       //if (ListEmployee.isSucess)
       //{
       //    return Ok();
       //  }
      //   return ValidationProblem();
        }
    }
      
       

    


//public void DeleteEmployeeById()

//public ActionResult Remove(int id)
//{
//Employee Emp = new Employee();
// ActionResult result = new ActionResult() { isSucess = true };

