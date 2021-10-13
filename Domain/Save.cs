using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Configuration;

namespace Domain
{
    public class Save
    {
        
      //public string connect = Configurationmanager.ConnectionStrings["connection"].ConnectionString;

       // public static object Configurationmanager { get; set; }

        public void save()
        {

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Employee>));
            TextWriter Filestream = new StreamWriter(@"C:\Users\EBRAHIM\source\repos\employees.xml");
            xmlSerializer.Serialize(Filestream, Employeemanager._employeeList);

            Filestream.Close();

            //FileStream stream = new FileStream(@"C:\Users\hp\source\repos\employees.xml", FileMode.Append, FileAccess.Write);
            //        xmlSerializer.Serialize(stream,xmlSerializer);
            //        stream.Close();
            XmlSerializer ProjectxmlSerializer = new XmlSerializer(typeof(List<Project>));
            TextWriter ProjectFilestream = new StreamWriter(@"C:\Users\EBRAHIM\source\repos\projects.xml");
            ProjectxmlSerializer.Serialize(ProjectFilestream, Projectmanager._projectList);

            ProjectFilestream.Close();
            XmlSerializer RolexmlSerializer = new XmlSerializer(typeof(List<Role>));
            TextWriter RoleFilestream = new StreamWriter(@"C:\Users\EBRAHIM\source\repos\roles.xml");
            RolexmlSerializer.Serialize(RoleFilestream, Rolemanager._roleList);

            RoleFilestream.Close();
        }
    }
      

}
