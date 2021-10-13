using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Ation;

namespace Domain
{
    public class Employeemanager : ioperation<Employee>
    {

        public static List<Employee> _employeeList = new List<Employee>();

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
            
            var resultEmp = Add(Emp);
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



        public ActionResult Add(Employee emp)
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
        //public interface IOperation<T>
        //Add Result Add(T t)
        //ListAll DataResult<T> ListAll()
        //ListById
        //Delete Result Remove(int id)
        public DataResult<Employee> ListAll()
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
            Console.WriteLine("Choose Employee From Below Employee List: Employee ID:Employee First Name");
            var resPro = ListAll();
            if (resPro.isSucess)
            {
                foreach (Employee res in resPro.Results)
                {
                    Console.WriteLine(res.Id + " : " + res.Name);
                }
            }
            else
            {
                Console.WriteLine(resPro.status);
            }
            Console.Write("Enter The Employee Id wchich u want delete ");
            int e2 = Convert.ToInt32(Console.ReadLine());
            Projectmanager p1 = new Projectmanager();
            var result1 = p1.isEmployeePresentinProject(e2);
            if (!result1.isSucess)
            {
                var result = Remove(e2);
                if (!result.isSucess)
                {
                    Console.WriteLine("Employee is not deleted");
                    Console.WriteLine(result.status);
                }
                else
                {
                    Console.WriteLine(result.status);
                }
            }
            else
            {
                Console.WriteLine(result1.status);
            }
        }
       
        public ActionResult Remove(int id)
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





