using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Ation;

namespace Domain
{
    public class projectmanager
    {
       
        private static List<Project> _projectList;
        static projectmanager()
        {
            _projectList = new List<Project>();
        }

       
        public ActionResult AddProject(Project pro)
        {
            ActionResult result = new ActionResult(){ isSucess = true};
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

        public DataResult<Project> GetprojectInfo()
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
            Project project = new Project();
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
   

