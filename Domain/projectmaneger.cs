using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Ation;

namespace Domain
{
    public class Projectmanager:ioperation<Project>
    {

        private static List<Project> _projectList = new List<Project>();
        public void AddProject()
        {
            Project project = new Project();
            try
            {


                Console.WriteLine("Enter project Id");
                project.id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter project Name");
                project.Name = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter project StartDate");
                project.StartDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter project EndDate");
                project.EndDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter project Budget");
                project.Budget = Convert.ToDecimal(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("error occured" + e.ToString());

            }

            var resultEmp = Add(project);
            if (!resultEmp.isSucess)
            {
                Console.WriteLine("Project failed to Add");
                Console.WriteLine(resultEmp.status);
            }
            else
            {
                Console.WriteLine(resultEmp.status);
            }
        }

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
            if(_projectList.Count>0)
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
                if(!result.isSucess)
                
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
        public  ActionResult Remove(int id)
            {
                Project project = new Project();
               ActionResult  result = new ActionResult() { isSucess = true };
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

    }
}
   

