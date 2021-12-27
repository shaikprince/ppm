using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Model;
using Model.Ation;

namespace Domain
{
    public class Projectmanager : IOperation<Project>
    {

        private static List<Project> _projectList = new List<Project>();
       

        public ActionResult Add(Project pro)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                _projectList.Add(pro);
                result.status = "Project added";
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured" + ex.ToString());
                result.isSucess = false;
            }
            return result;
        }

        public DataResult<Project> ListAll()
        {
            DataResult<Project> data = new DataResult<Project> { isSucess = true };
            if (_projectList.Count > 0)
            {
                data.Results = _projectList;
            }
            else
            {
                data.isSucess = false;
                data.status = "List is Empty!";
            }
            return data;
        }
        public void DeleteProjectById()
        {


            Console.WriteLine("Choose Project From Below Project List: Project ID:Project Name");
            var resPro = ListAll();
            if (resPro.isSucess)
            {
                foreach (Project res in resPro.Results)
                {
                    Console.WriteLine(res.id + " : " + res.Name);
                }
            }
            else
            {
                Console.WriteLine(resPro.status);
            }
            Console.Write("Enter The project Id wchich u want delete ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = Remove(id);
            if (!result.isSucess)

            {
                Console.WriteLine("project not deleted");
                Console.WriteLine(result.status);
            }
            else
            {
                Console.WriteLine(result.status);
            }
        }
        public void AddEmployeeToProject()
        {
            Employeemanager m1 = new Employeemanager();
            Employee employee = new Employee();
            Console.WriteLine("Choose Project From Below Project List: Project ID:Project Name");
            var resPro = ListAll();
            if (resPro.isSucess)
            {
                foreach (Project result in resPro.Results)
                {
                    Console.WriteLine(result.id + " : " + result.Name);
                }
            }
            else
            {
                Console.WriteLine(resPro.status);
            }
            Console.Write("Provide the project Id: ");
            int projectId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Below is the Employee ID and respective Name to choose:");
            var res = m1.ListAll();
            if (res.isSucess)
            {
                foreach (Employee e in res.Results)
                {
                    Console.WriteLine(e.Id + " : " + e.Name);
                }
            }
            else
            {
                Console.WriteLine(res.status);
            }
            Console.Write("Enter the Id of the employee: ");
            employee.Id = Convert.ToInt32(Console.ReadLine());
            var valid = m1.isvalidEmp(employee);
            if (valid.isSucess)
            {


                var obj = m1.GetEmployeetoRole(employee);
                employee.RoleName = obj.RoleName;
                employee.Name = obj.Name;
                var result = AddEmployeetoProject(employee, projectId);


                if (!result.isSucess)
                {
                    Console.WriteLine("Failed to Add Employee into project");
                    Console.WriteLine(result.status);
                }
                else
                {
                    Console.WriteLine(result.status);
                }
            }
            else
            {
                Console.WriteLine(valid.status);
            }
        }
        public ActionResult Remove(int id)
        {
            Project project = new Project();
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                if (_projectList.Exists(p => p.id == id))
                {
                    var itemToRemove = _projectList.Single(s => s.id == id);
                    _projectList.Remove(itemToRemove);
                    result.status = "Project is Deleted Successfully " + project.id;
                }
                else
                {
                    result.isSucess = false;
                    result.status = "Project Id is not in the List!" + id;
                }
            }
            catch (Exception e)
            {
                result.isSucess = false;
                result.status = "Exception Occured : " + e.ToString();
            }
            return result;
        }
        public ActionResult isEmployeePresentinProject(int Id)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            uint count = 0;
            if (_projectList.Count > 0)
            {
                foreach (Project Pro in _projectList)
                {
                    if (Pro.Emplist.Exists(p => p.Id == Id))
                    {
                        count++;
                    }
                }
                if (count > 0)
                {
                    result.status = "Employee is present!";
                }
                else
                {
                    result.isSucess = false;
                    result.status = "Employee is not present in any project!";
                }
            }
            else
            {
                result.isSucess = false;
            }
            return result;

        }


        public ActionResult AddEmployeetoProject(Employee employee, int id)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                if (_projectList.Count > 0)
                {
                    if (_projectList.Exists(p => p.id == id))
                    {
                        if (_projectList.Single(p => p.id == id).Emplist == null)
                        {
                            _projectList.Single(p => p.id == id).Emplist = new List<Employee>();
                        }

                        if (_projectList.Single(p => p.id == id).Emplist.Exists(e => e.Id == employee.Id))
                        {
                            result.status = $"Employee Id : {employee.Id} already exists in this project: {id}";
                        }
                        else
                        {
                            _projectList.Single(p => p.id == id).Emplist.Add(employee);
                            result.status = "Employee is Added to project";

                        }

                    }
                    else
                    {
                        result.isSucess = false;
                        result.status = "Project Id not found!" + id;
                    }
                }
                else
                {
                    result.isSucess = false;
                    result.status = "Project list is Empty!";
                }

            }
            catch (Exception e)
            {
                result.isSucess = false;
                result.status = "Exception Occured : " + e.ToString();
            }
            return result;
        }



        public ActionResult DeleteEmployeefromProject(Employee emp, int id)
        {

            ActionResult action = new ActionResult() { isSucess = true };
            try
            {
                if (_projectList.Exists(p => p.id == id))
                {
                    if (_projectList.Single(s => s.id == id).Emplist.Exists(n => n.Id == emp.Id))
                    {
                        var itemToRemove = _projectList.Single(s => s.id == id).Emplist.Single(e => e.Id == emp.Id);
                        _projectList.Single(s => s.id == id).Emplist.Remove(itemToRemove);
                        action.status = "Employee is Deleted Successfully " + emp.Id;
                    }
                    else
                    {
                        action.isSucess = false;
                        action.status = "Given Employee ID is not Present in the particular Project " + emp.Id;
                    }
                }
                else
                {
                    action.isSucess = false;
                    action.status = "Project Id is not in the List!" + id;
                }
            }
            catch (Exception e)
            {
                action.isSucess = false;
                action.status = "Exception Occured : " + e.ToString();
            }
            return action;
        }
        public ActionResult ToXmlSerialization(string fileName)
        {
            ActionResult actionResult = new ActionResult() { isSucess = true };
            try
            {
                if (_projectList.Count > 0)
                {
                    //string filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("F:\\PPM\\Model"), "AppData", fileName)
                    // var filePath = System.IO.File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + fileName)
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        serializer.Serialize(tw, _projectList);
                        tw.Close();
                    }



                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Role list is Empty";
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
                if (_projectList.Count > 0)
                {
                    using (TextWriter sw = new StreamWriter(fileName))
                    {
                        foreach (Project pro in _projectList)
                        {
                            sw.WriteLine("Project ID: " + pro.id + "\nProject Name: " + pro.Name + "\nStarting Date: " + pro.StartDate.ToShortDateString() +
                            "\nEnding Date: " + pro.EndDate.ToShortDateString() + "\nBudget: " + pro.Budget);
                            sw.WriteLine("Employee Assigned:");
                            if (pro.Name != null)
                            {
                                foreach (Employee e in pro.Emplist)
                                {
                                    sw.WriteLine("Employee Id: " + e.Id + " " + "|" + "Employee Name : " + e.Name + " " + "|" + "Role : " + e.RoleName);
                                }
                            }
                            else
                            {
                                sw.WriteLine("No Employee Assigned!");
                            }
                            sw.WriteLine("-------------------------------------------------------");
                            actionResult.status = "Project Is Saved in The Text File!";
                        }
                    }
                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Project list is empty!";
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
            string str = "DROP TABLE IF EXISTS project";
            try
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(str, myConn))
                {
                    command.ExecuteNonQuery();
                    actionResult.status = "Old Data Dropped Successfully!";
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
                    using (SqlCommand command = new SqlCommand("CREATE TABLE project (ProjectId int,ProjectName varchar(50), StartDate datetime, EndDate datetime, Budget decimal, EmpId int);", myConn))
                    {
                        command.ExecuteNonQuery();



                        foreach (Project project in _projectList)
                        {
                            foreach (Employee emp in project.Emplist)
                            {
                                int id = (int)project.id;
                                int EmpId = (int)emp.Id;
                                string insertQ = "INSERT INTO project values(@ProjectId,@ProjectName,@StartDate, @EndDate, @Budget,@EmpId)";
                                SqlCommand command1 = new SqlCommand(insertQ, myConn);
                                command1.Parameters.AddWithValue("@ProjectId", project.id);
                                command1.Parameters.AddWithValue("@ProjectName", project.Name);
                                command1.Parameters.AddWithValue("@StartDate", project.StartDate);
                                command1.Parameters.AddWithValue("@EndDate", project.EndDate);
                                command1.Parameters.AddWithValue("@Budget", project.Budget);
                                command1.Parameters.AddWithValue("@EmpId", EmpId);
                                command1.ExecuteNonQuery();
                            }
                        }
                        actionResult.status = actionResult.status + "\n" + "Table project Added SuccessFully!";
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


        public DataResult<Project> ToEFDB()
        {
            DataResult<Project> actionResult = new DataResult<Project>() { isSucess = true };
            try
            {
                if (_projectList.Count > 0)
                {
                    using (var db = new Context())
                    {
                        Project project1 = new Project();
                        List<Project> projectList = db.Projects.ToList();
                        foreach (Project project in _projectList)
                        {
                            project1.id = project.id;
                            project1.Name = project.Name;
                            project1.Budget = project.Budget;
                            project1.EndDate = Convert.ToDateTime(project.EndDate.ToShortDateString());
                            project1.StartDate = Convert.ToDateTime(project.StartDate.ToShortDateString());
                            if (projectList.Exists(p => p.id == project.id))
                            {
                                var p = projectList.Single(p => p.id == project.id);
                                db.Projects.Remove(p);
                                db.SaveChanges(); db.Projects.Add(project1);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Projects.Add(project1);
                                db.SaveChanges();
                            }
                        }
                    }
                    actionResult.status = "Project Saved to Database Successfully";
                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Project List is Empty!";
                }
            }
            catch (Exception e)
            {
                actionResult.isSucess = false;
                actionResult.status = e.Message;
            }
            return actionResult;

        }
    }
}
   

