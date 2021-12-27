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
    public class Rolemanager:IOperation<Role>
    {
        private static List<Role> _roleList = new List<Role>();
        
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
        public ActionResult ToXmlSerialization(string fileName)
        {
            ActionResult actionResult = new ActionResult() { isSucess = true };
            try
            {
                if (_roleList.Count > 0)
                {
                    //string filePath = System.IO.Path.Combine(System.Web.HttpContext.Current.Server.MapPath("F:\\PPM\\Model"), "AppData", fileName)
                    // var filePath = System.IO.File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + fileName)
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        serializer.Serialize(tw, _roleList);
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
                if (_roleList.Count > 0)
                {
                    using (TextWriter sw = new StreamWriter(fileName))
                    {
                        foreach (Role r in _roleList)
                        {
                            sw.WriteLine("Role Id: " + r.RoleId + "\nRole Name: " + r.RoleName);
                            sw.WriteLine("--------------------------------------------------------------------------------------");
                            actionResult.status = "Role Is Saved in The Text File!";
                        }
                    }
                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Role list is empty!";
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
            string str = "DROP TABLE IF EXISTS role";
            try
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(str, myConn))
                {
                    command.ExecuteNonQuery();
                    actionResult.status = "Old Data of Role Dropped Successfully!";
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
                    using (SqlCommand command = new SqlCommand("CREATE TABLE role (RoleId int,RoleName varchar(50));", myConn))
                    {
                        command.ExecuteNonQuery();



                        foreach (Role role in _roleList)
                        {
                            int id = (int)role.RoleId;
                            string insertQ = "INSERT INTO role values(@RoleId,@RoleName)";
                            SqlCommand command1 = new SqlCommand(insertQ, myConn);
                            command1.Parameters.AddWithValue("@RoleId", id);
                            command1.Parameters.AddWithValue("@RoleName", role.RoleName);
                            command1.ExecuteNonQuery();
                        }
                        actionResult.status = actionResult.status + "\n" + "Table role Added SuccessFully!";
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
                if (_roleList.Count > 0)
                {
                    using (var db = new Context())
                    {
                        List<Role> roleList = db.Roles.ToList();
                        foreach (Role role in _roleList)
                        {
                            if (roleList.Exists(r => r.RoleId == role.RoleId))
                            {
                                var r = roleList.Single(r => r.RoleId == role.RoleId);
                                db.Roles.Remove(r);
                                db.SaveChanges(); db.Add(role);
                                db.SaveChanges();
                            }
                            else
                            {
                                db.Roles.Add(role);
                                db.SaveChanges();
                            }
                        }
                        actionResult.status = "Role Added to Database Successfully!";
                    }
                }
                else
                {
                    actionResult.isSucess = false;
                    actionResult.status = "Role List is Empty!";
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
    
       
    

