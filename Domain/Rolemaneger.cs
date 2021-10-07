using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Ation;

namespace Domain
{
    public class Rolemanager:ioperation<Role>
    {
        private static List<Role> _roleList = new List<Role>();
        public void AddRole()
        {
            Role role = new Role();
            try
            {
                
                Console.WriteLine("Enter Role Id");
                role.RoleId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Role Name");
               role.RoleName = Convert.ToString(Console.ReadLine());
                
            }
            catch (Exception e)
            {
                Console.WriteLine("error occured" + e.ToString());
            }
            var resultRole = Add(role);
            if (!resultRole.isSucess)
            {
                Console.WriteLine("Role failed to Add");
                Console.WriteLine(resultRole.status);
            }
            else
            {
                Console.WriteLine(resultRole.status);
            }
        }
        public ActionResult Add(Role role)
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

        public DataResult<Role> ListAll()
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
        public void DeleteRoleById()
        {
            

            Console.WriteLine("Choose Role From Below Role List: Role ID:Role Name");
            var resRole = ListAll();
            if (resRole.isSucess)
            {
                foreach (Role e2 in resRole.Results)
                {
                    Console.WriteLine(e2.RoleId + " : " + e2.RoleName);
                }
            }
            else
            {
                Console.WriteLine(resRole.status);
            }
            Console.Write("Enter The Role Id wchich u want delete ");
            int id = Convert.ToInt32(Console.ReadLine());
            var result = Remove(id);
            if (!result.isSucess)
            {
                Console.WriteLine("Role not deleted");
                Console.WriteLine(result.status);
            }
            else
            {

                Console.WriteLine(result.status);
            }
        }
        public  ActionResult Remove(int id)
        {
            Role role = new Role();
            ActionResult result = new ActionResult() { isSucess = true };
            try
            {
                if (_roleList.Exists(r => r.RoleId == id))
                {
                    var itemToRemove = _roleList.Single(s => s.RoleId == id);
                    _roleList.Remove(itemToRemove);
                    result.status = "Role is Deleted Successfully " + role.RoleId;
                }
                else
                {
                    result.isSucess = false;
                    result.status = "Role Id is not in the List!" + id;
                }
            }
            catch (Exception e)
            {
                result.isSucess = false;
                result.status = "Exception Occured : " + e.ToString();
            }
            return result;
        }
    
       
    }
}
