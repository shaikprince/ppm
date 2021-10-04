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

        private static List<Employee> _employeeList = new List<Employee>();

        public void AddEmployee()
        {
            Employee Emp = new Employee();
            try
            {
               

                Console.WriteLine("Enter Employee Id");
                Emp.Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name");
               Emp.Name = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter Employee Contact ");
               Emp.Contact=Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter employee email");
                Emp.Email = Console.ReadLine();
              
               
            }
            catch (Exception e)
            {
                Console.WriteLine("error occured" + e.ToString());
            }
            
            var resultEmp = AddEmployee(Emp);
            if (!resultEmp.isSucess)
            {
                Console.WriteLine("Employee failed to Add");
                Console.WriteLine(resultEmp.status);
            }
            else
            {
                Console.WriteLine(resultEmp.status);
            }
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
        public void DeleteEmployeeById()
        {
            

            Console.WriteLine("Choose Employee From Below Employee List: Employee ID:Employee Name");
            var resEmp = GetEmployeeInfo();
            if (resEmp.isSucess)
            {
                foreach (Employee e2 in resEmp.Results)
                {
                    Console.WriteLine(e2.Id + " : " + e2.Name);
                }
            }
            else
            {
                Console.WriteLine(resEmp.status);
            }
            Console.Write("Enter The Employee Id wchich u want delete ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = RemoveEmployee(id);
            if (!result.isSucess)
            {
                Console.WriteLine("Employee not deleted");
                Console.WriteLine(result.status);
            }
            else
            {
                Console.WriteLine(result.status);
            }
        }
        public static ActionResult RemoveEmployee(int id)
        {
            Employee Emp = new Employee();
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                if (_employeeList.Exists(Emp => Emp.Id == id))
                {
                    var itemToRemove = _employeeList.Single(s => s.Id == id);
                    _employeeList.Remove(itemToRemove);
                    result.status = "Employee is Deleted Successfully " + Emp.Id;
                }
                else
                {
                    result.isSucess = false;
                    result.status = "Employee Id is not in the List!" + id;
                }
            }
            catch (Exception e)
            {
                result.isSucess = false;
                result.status = "Exception Occured : " + e.ToString();
            }
            return result;
        }
        public ActionResult isvalidEmp(Employee empname)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            return result;
        }
        public Employee GetEmployeetoRole(Employee employeeId)
        {
            Employee emp = new Employee();
            emp.RoleName = _employeeList.Single(e => e.Id == employeeId.Id).RoleName;
            emp.Name = _employeeList.Single(e => e.Id == employeeId.Id).Name;
            return emp;
        }

    }
    }





