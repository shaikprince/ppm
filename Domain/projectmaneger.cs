using System;
using System.Collections.Generic;
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
    }
}
