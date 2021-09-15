using System;
using System.Collections.Generic;
using Model;
using Model.Ation;

namespace Domain
{
    public class Rolemanager
    {
        private static List<Role> _roleList;
        static Rolemanager()
        {
            _roleList = new List<Role>();
        }
        public ActionResult AddRole(Role role)
        {
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                _roleList.Add(role);
                result.status = "Role added";
            }
            catch (Exception ex)
            {
                Console.WriteLine("error occured" + ex.ToString());
                result.isSucess = false;
            }
            return result;
        }

        public DataResult<Role> GetRoleInfo()
        {
            DataResult<Role> data = new DataResult<Role> { isSucess = true };
            if (_roleList.Count > 0)
            {
                data.Results = _roleList;
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
