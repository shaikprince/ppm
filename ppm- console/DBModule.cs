using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain;




namespace ppm__console
{
    public class DBModule
    {
        public void DB_ADO()
        {
            Employeemanager employeemanager = new Employeemanager();
            Projectmanager projectmanager = new Projectmanager();
            Rolemanager rolemanager = new Rolemanager();
            var saveRoleToDB = rolemanager.ToAdoDB();
            var saveEmployee = employeemanager.ToAdoDB();
            var saveProject = projectmanager.ToAdoDB();
            if (saveRoleToDB.isSucess || saveEmployee.isSucess || saveProject.isSucess)
            {
                Console.WriteLine(saveRoleToDB.status + "\n" + saveEmployee.status + "\n" + saveProject.status);
                Console.WriteLine("Saved To Database Sucessfully!");
            }
            else
            {
                Console.WriteLine(saveRoleToDB.status);
            }



        }
        public void DB_EF()
        {
            static String GetTimestamp(DateTime value)
            {
                return value.ToString("yyyy/MM/dd/HH:mm:ss:dffff");
            }
            String timeStamp = GetTimestamp(DateTime.Now);
            Employeemanager employeeManager = new Employeemanager();
            Projectmanager projectManager = new Projectmanager();
            Rolemanager roleManager = new Rolemanager();
            var saveRoleToDB = roleManager.ToEFDB();
            var saveEmployeeToDB = employeeManager.ToEFDB();
            var saveProjectToDB = projectManager.ToEFDB();
            if (saveRoleToDB.isSucess || saveProjectToDB.isSucess)
            {
                Console.WriteLine(saveRoleToDB.status + " \n " + saveProjectToDB.status + " \n " + saveEmployeeToDB.status);
                Console.WriteLine("Saved To Database Successfully!");
                Console.WriteLine(timeStamp);
            }
            else
            {
                Console.WriteLine(saveRoleToDB.status + "\n" + saveEmployeeToDB.status + "\n" + saveProjectToDB.status);
                //Console.WriteLine(saveRoleToDB.Status + "\n" + saveProjectToDB.Status)
            }
        }
    }
}
