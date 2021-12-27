using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Domain;
using Model;
using Web.Controllers;

namespace ppm__console
{
    public  class Command
    {
        public static void startprogram()

        {

            Console.WriteLine("following operation");
            Console.WriteLine("1.Project Module");
            Console.WriteLine("2.Employee Module");
            Console.WriteLine("3.Role Module");
            Console.WriteLine("4. Save");
            Console.WriteLine("5.Quit");
            int i = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("choose the given option: ");
                    i = Convert.ToInt32(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            ProjectModule();
                            break;
                        case 2:
                            EmployeeModule();
                            break;
                        case 3:
                            RoleModule();
                            break;
                        case 4:
                         save();
                            break;
                        default:
                            Console.WriteLine("Option is not in the list!");
                            break;
                    }
                }
                catch (Exception) 
                {
                    Console.WriteLine("oops! Error Occured! Try Again");
                    startprogram();

                }

            }
        }

       

        public static void ProjectModule()
        {
            Console.WriteLine("Choose the option you want to select:");
            Console.WriteLine("Press 1: Add Project");
            Console.WriteLine("Press 2: List Project");
            Console.WriteLine("Press 3: List Project by Id");
            Console.WriteLine("Press 4: Delete Project");
            Console.WriteLine("Press 5: Add employee to Project");
            Console.WriteLine("Press 6: Go to main Menu");
            int j = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Choose Your Option from 1 to 6: ");
                    j = Convert.ToInt32(Console.ReadLine());
                    Projectmanager v2 = new Projectmanager();
                    switch (j)
                    {
                        case 1:
                        Command.AddProject();
                            break;
                        case 2:
                            Console.WriteLine("Project Details: ");
                            var ResPro = v2.ListAll();
                            if (ResPro.isSucess)
                            {
                                foreach (Project result1 in ResPro.Results)
                                {
                                    Console.WriteLine("Project id: " + result1.id + "\nProject Name: " + result1.Name + "\nstarting date: " + result1.StartDate + "\nEnd Date:" + result1.EndDate + "\nBudget :" + result1.Budget);
                                }

                            }
                            else
                            {
                                Console.WriteLine(ResPro.status);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter project id which u want to display");
                            int n1 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Project Details:");
                            var ResPro1 = v2.ListAll();
                            if (ResPro1.isSucess)
                            {
                                foreach (Project result1 in ResPro1.Results)
                                {
                                    if (result1.id == n1)
                                    {
                                        Console.WriteLine("Project id: " + result1.id + "\nProject Name: " + result1.Name + "\nstarting date: " + result1.StartDate + "\nEnd Date:" + result1.EndDate + "\nBudget :" + result1.Budget);

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(ResPro1.status);
                            }
                            break;
                        case 4:
                            v2.DeleteProjectById();
                            break;
                        case 5:
                            v2.AddEmployeeToProject();
                            break;
                        case 6:
                            startprogram();
                            break;
                        default:
                            Console.WriteLine("OOPS!Error occured! Try Again");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("OOPS!Error Ocurred! try again");
                    startprogram();
                }
            }
        }

        public static void EmployeeModule()
        {
            Console.WriteLine("Choose the Option you want to select:");
            Console.WriteLine("Press 1: Add Employee");
            Console.WriteLine("Press 2: List All Employee");
            Console.WriteLine("Press 3: List Employee by Id");
            Console.WriteLine("Press 4: Delete Employee");
            Console.WriteLine("Press 5: Return to main Menu");
            int j = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Choose Your option from 1 to 5: ");
                    j = Convert.ToInt32(Console.ReadLine());
                    Employeemanager e1 = new Employeemanager();
                    switch (j)
                    {
                        case 1:
                            Command.AddEmployee();
                            break;
                        case 2:
                            Console.WriteLine("Employee Details: ");
                            var ResEmp = e1.ListAll();
                            if (ResEmp.isSucess)
                            {
                                foreach (Employee e2 in ResEmp.Results)
                                {
                                    Console.WriteLine("Employee id: " + e2.Id + "\nEmployee_Name Name: " + e2.Name + "\nContact: " + e2.Contact + "\nEmail :" + e2.Email);

                                }

                            }
                            else
                            {
                                Console.WriteLine(ResEmp.status);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter Employee id which u want to display");
                            int E1 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Employee Details: ");
                            var ResEmp1 = e1.ListAll();
                            if (ResEmp1.isSucess)
                            {
                                foreach (Employee e2 in ResEmp1.Results)
                                {
                                    if (e2.Id == E1)
                                    {
                                        Console.WriteLine("Employee id: " + e2.Id + "\nEmployee_Name Name: " + e2.Name + "\nContact: " + e2.Contact + "\nEmail :" + e2.Email);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(ResEmp1.status);
                            }
                            break;
                        case 4:
                            e1.DeleteEmployeeById();
                            break;
                        case 5:
                            startprogram();
                            break;
                        default:
                            Console.WriteLine("OOPS ! Error Occoured! Try Again");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("OOPS Error occured! try Again");
                }
            }
        }
        public  static void RoleModule()
        {
            Console.WriteLine("Choose the Option you want to select:");
            Console.WriteLine("Press 1: Add Role");
            Console.WriteLine("Press 2: List All Role");
            Console.WriteLine("Press 3: List Role by Id");
            Console.WriteLine("Press 4: Delete Role");
            Console.WriteLine("Press 5: GO to main Menu");
            int j = 0;
            while (true)
            {
                try
                {
                    Console.WriteLine("Choose your options: ");
                    j = Convert.ToInt32(Console.ReadLine());
                    Rolemanager m3 = new Rolemanager();
                    switch (j)
                    {
                        case 1:
                            Command.AddRole();
                            break;
                        case 2:
                            Console.WriteLine("Role Details: ");
                            var Resrole = m3.ListAll();
                            if (Resrole.isSucess)
                            {
                                foreach (Role e2 in Resrole.Results)
                                {
                                    Console.WriteLine("Role Id: " + e2.RoleId + "\nRole Name: " + e2.RoleName);

                                }

                            }
                            else
                            {
                                Console.WriteLine(Resrole.status);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Enter project id which u want to display");
                            int n1 = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Project Details:");
                            var Resrole1 = m3.ListAll();
                            if (Resrole1.isSucess)
                            {
                                foreach (Role e2 in Resrole1.Results)
                                {
                                    if (e2.RoleId == n1)
                                    {
                                        Console.WriteLine("Role Id: " + e2.RoleId + "\nRole Name: " + e2.RoleName);

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(Resrole1.status);
                            }
                            break;
                        case 4:
                            m3.DeleteRoleById();
                            break;
                        case 5:
                            startprogram();
                            break;
                        default:
                            Console.WriteLine("optipon is in the list!");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("oops! Error occured! Try Again");
                }

            }
        }
        public static void save()
        {
            try
            {
                Console.WriteLine("Press 1: Save as XML file");
                Console.WriteLine("Press 2: Save as TXT File");
                Console.WriteLine("Press 3: Save as DB-Ado");
                Console.WriteLine("Press 4: Save as DB-EF");
                Console.Write("Please Choose the Save Method: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                Employeemanager employeemanager = new Employeemanager();
                Projectmanager projectmanager = new Projectmanager();
                Rolemanager rolemanager = new Rolemanager();
                switch (choice)
                {



                    case 1:
                        Console.WriteLine("---SAVE AS XML FILE---");



                        var employeeSerialize = employeemanager.ToXmlSerialization("Employee.xml");
                        var projectSerialize = projectmanager.ToXmlSerialization("Project.xml");
                        var roleSerialize = rolemanager.ToXmlSerialization("Role.xml");



                        if (employeeSerialize.isSucess || projectSerialize.isSucess || roleSerialize.isSucess)
                        {
                            Console.WriteLine("Save Data Sucessfully!");
                            Console.WriteLine(employeeSerialize.status + "\n" + projectSerialize.status + "\n" + roleSerialize.status);
                        }
                        else
                        {
                            Console.WriteLine(employeeSerialize.status + "\n" + projectSerialize.status + "\n" + roleSerialize.status);
                        }
                        break;
                    case 2:
                        Console.WriteLine("---SAVE AS TEST FILE---");
                        var saveRoleToText = rolemanager.ToTxtFile("role.txt");
                        var saveEmployeeToText = employeemanager.ToTxtFile("SaveEmployee.txt");
                        var saveProjectToText = projectmanager.ToTxtFile("SaveProject.txt");
                        if (saveRoleToText.isSucess || saveEmployeeToText.isSucess || saveProjectToText.isSucess)
                        {
                            Console.WriteLine("Save Data To TEXT File Sucessfully!");
                            Console.WriteLine(saveRoleToText.status + "\n" + saveProjectToText.status + "\n" + saveEmployeeToText.status);
                        }
                        else
                        {
                            Console.WriteLine(saveRoleToText.status + "\n" + saveProjectToText.status + "\n" + saveEmployeeToText.status);
                        }
                        break;
                    case 3:
                        Console.WriteLine("---Save AS DB-ADO METHOD---");
                        DBModule dB = new DBModule();
                        dB.DB_ADO();
                        break;
                    case 4:
                        Console.WriteLine("---SAVE AS DB-EF METHOD---");
                        DBModule dB1 = new DBModule();
                        dB1.DB_EF();
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Oops, Error Occoured at Save State!");
                Console.WriteLine("-----------------------------------------------------");
                startprogram();

            }

        }
        
        public static void AddRole()
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
            RoleController roleController = new();
            roleController.AddRole(role);
        }
        public static void AddProject()
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
            ProjectController projectController = new();
            projectController.AddProject(project);
        }
        public  static void AddEmployee()
        {
            Employee Emp = new Employee();
            try
            {


                Console.WriteLine("Enter Employee Id");
                Emp.Id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Name");
                Emp.Name = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Enter Employee Contact ");
                Emp.Contact = Convert.ToInt64(Console.ReadLine());
                Console.WriteLine("Enter employee email");
                Emp.Email = Console.ReadLine();


            }
            catch (Exception e)
            {
                Console.WriteLine("error occured" + e.ToString());
            }
            EmployeeController employeeController = new();
           employeeController.AddEmployee(Emp);

        }

    }
   
    }


       
    

