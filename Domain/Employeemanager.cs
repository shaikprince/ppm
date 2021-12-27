using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Model;
using System.Data.SqlClient;
using System.Data;
using Model.Ation;

namespace Domain
{
    public  class Employeemanager : IOperation<Employee>
    {

        private static List<Employee> _employeeList = new List<Employee>();
       

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
        public ActionResult ToXmlSerialization(string fileName)
        {
           ActionResult  actionResult = new ActionResult() { isSucess = true };
            try
            {
                if (_employeeList.Count > 0)
                {
                    //string filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("F:\\PPM\\Model"), "AppData", fileName)
                    //var filePath = System.IO.File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + fileName)
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        serializer.Serialize(tw, _employeeList);
                        tw.Close();
                    }



                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Employee List is Empty!";
                }
            }
            catch (Exception e)
            {
                actionResult.isSucess = false;
                actionResult.status = "Error Occoured!" + e.Message;
            }
            return actionResult;
        }
       
        public ActionResult ToTxtFile(string fileName)
        {
            ActionResult actionResult = new ActionResult() { isSucess = true };
            try
            {
                if (_employeeList.Count > 0)
                {
                    using (TextWriter sw = new StreamWriter(fileName))
                    {
                        foreach (Employee e in _employeeList)
                        {
                            sw.WriteLine("Employee Id: " + e.Id + "\nEmployee Name: " + e.Name + "\nContact Number : " + e.Contact + "\nEmail :"
                            + e.Email + "\nRole Id: " + e.Id);
                            sw.WriteLine("--------------------------------------------------------------------------------------");
                            actionResult.status = "Employee Is Saved in The Text File!";
                        }
                    }
                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Employee list is empty!";
                }
            }
            catch (Exception e)
            {
                actionResult.isSucess = false;
                actionResult.status = "Error Occoured" + "\n" + e.Message;
            }



            return actionResult;
        }
        public ActionResult ToAdoDB()
        {
            ActionResult actionResult = new ActionResult() { isSucess = true };
            string conn = "Server=(localdb)\\MSSQLLocalDB; Database=PPM;Integrated security=true;TrustServerCertificate=true";
            SqlConnection myConn = new SqlConnection(conn);
            string str = "DROP TABLE IF EXISTS employee";
            try
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(str, myConn))
                {
                    command.ExecuteNonQuery();
                    actionResult.status = "Old Data of Employee Dropped Successfully!";
                }
            }
            catch (Exception e)
            {
                actionResult.isSucess = false;
                actionResult.status = e.Message;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            if (actionResult.isSucess)
            {
                try
                {
                    myConn.Open();
                    using (SqlCommand command = new SqlCommand("CREATE TABLE employee (Id int,EmployeeName varchar(50), DOB datetime, Contact bigint, RoleName varchar(50));", myConn))
                    {
                        command.ExecuteNonQuery();



                        foreach (Employee employee in _employeeList)
                        {
                            int id = (int)employee.Id;
                            long contact = (long)employee.Contact;
                            string insertQ = "INSERT INTO employee values(@Id,@EmployeeName,@DOB,@Contact,@RoleName)";
                           SqlCommand command1 = new SqlCommand(insertQ, myConn);
                            command1.Parameters.AddWithValue("@Id", id);
                            command1.Parameters.AddWithValue("@EmployeeName", employee.Name);
                            command1.Parameters.AddWithValue("@Contact", contact);
                            command1.Parameters.AddWithValue("@Email", employee.Email);
                            command1.Parameters.AddWithValue("@RoleId", employee.Id);
                            command1.ExecuteNonQuery();
                        }
                        actionResult.status = actionResult.status + "\n" + "Table employee Added SuccessFully!";
                    }



                }
                catch (Exception e)
                {
                    actionResult.isSucess = false;
                    actionResult.status = e.Message;
                }
                finally
                {
                    if (myConn.State == ConnectionState.Open)
                    {
                        myConn.Close();
                    }
                }
            }
            return actionResult;
        }
       
public ActionResult ToEFDB()
        {
            ActionResult actionResult = new ActionResult() { isSucess = true };
            try
            {
                using (var db = new Context())
                {
                    foreach (Employee employee in _employeeList)
                    {
                        db.Employees.Add(employee);
                        db.SaveChanges();
                    }
                }
                actionResult.status = "Employee Saved to Database Successfully";
            }
            catch (Exception e)
            {
                actionResult.isSucess = true;
                actionResult.status = e.Message;
            }
            return actionResult;
        }
    }

}





    
    





