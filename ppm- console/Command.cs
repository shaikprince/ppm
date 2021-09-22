using System;
using Domain;
using Model;
namespace ppm__console
{
    public class Command
    {
        public void startprogram()

        {
            projectmanager pro = new projectmanager();
            Console.WriteLine("following operation");
            Console.WriteLine("1.Add project");
            Console.WriteLine("2.View project");
            Console.WriteLine("3.Add Employee");
            Console.WriteLine("4.View Employee");
            Console.WriteLine("5.Add Role");
            Console.WriteLine("6.View Role");
            Console.WriteLine("7.Add Employee to Project");
            Console.WriteLine("8.Delete Employee from Project");
            Console.WriteLine("9.view project details");
            Console.WriteLine("10.Quit");

            bool i = true;
            while (i)
            {
                Console.Write("select from any option: ");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {

                    case 1:
                        AddProject();
                        break;
                    case 2:
                       // projectmanager pro = new projectmanager();
                        var pres = pro.GetprojectInfo();

                        int c = 0;
                        if (pres.isSucess)
                        {
                            foreach (Project p in pres.Results)
                            {
                                Console.WriteLine("Project " + c);
                                Console.WriteLine("projectId:" + p.id);
                                Console.WriteLine("projectName:" + p.Name);
                                Console.WriteLine("projectStartDate:" + p.StartDate);
                                Console.WriteLine("projectEndDate:" + p.EndDate);
                                Console.WriteLine("projectBudget:" + p.Budget);
                                c++;
                            }
                        }
                        else
                        {
                            Console.WriteLine(pres.status);
                        }
                        break;
                    case 3:
                        AddEmployee();
                        break;
                    case 4:
                        Employeemanager employeemanager = new Employeemanager();
                        var employee = employeemanager.GetEmployeeInfo();
                        int C = 0;
                        if (employee.isSucess)
                        {
                            foreach (Employee emp in employee.Results)
                            {

                                Console.WriteLine("Employee no " + i);
                                Console.WriteLine("Employee id:" + emp.Id);
                                Console.WriteLine("Employee Name:" + emp.Name);
                                Console.WriteLine("Employee Contact:" + emp.Contact);
                                Console.WriteLine("Employee Email:" + emp.Email);
                                C++;
                            }
                        }
                        else
                        {
                            Console.WriteLine(employee.status);
                        }
                        break;

                    case 5:
                        AddRole();
                        break;
                    case 6:
                        Rolemanager roleMgr = new Rolemanager();
                        var roleInfoResult = roleMgr.GetRoleInfo();
                        int count = 0;
                        if (roleInfoResult.isSucess)
                        {
                            foreach (Role r in roleInfoResult.Results)
                            {
                                Console.WriteLine("Role no " + count);
                                Console.WriteLine("Role id:" + r.RoleId);
                                Console.WriteLine("Role Name:" + r.RoleName);

                                count++;
                            }
                        }
                        else
                        {
                            Console.WriteLine(roleInfoResult.status);
                        }
                        break;
                    case 7:
                        Console.WriteLine("Add Employee to Project");
                        AddEmployeetoProject();
                        break;
                    case 8:
                        Console.WriteLine("Delete Employee from Project");
                        DeleteEmployeefromProject();
                        break;
                    case 9:
                        Console.WriteLine("Project Details with Employee Assigned:");
                       
                        var result = pro.GetprojectInfo();
                        if (result.isSucess)
                        {
                            foreach (Project Result in result.Results)
                            {
                                Console.WriteLine("Project ID: " + Result.id + "\nProject Name: " + Result.Name + "\nStarting Date: " + Result.StartDate + "\nEndDate: " + Result.EndDate + "\nBudget: " + Result.Budget);
                                Console.WriteLine("Employee Assigned: ");
                                if (Result.Emplist != null)
                                {
                                    foreach (Employee emp in Result.Emplist)
                                    {
                                        Console.WriteLine("Employee Id: " + emp.Id + " " + "Employee First Name: " + emp.Name + " " + "Employee Contact: " + emp.Contact);
                                    }
                                }

                            }
                        }
                        break;
                    case 10:
                        Environment.Exit(0);
                        break;



                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }



        }

        private static bool AddEmployeetoProject()
        {
            Employee emp = new Employee();
            Console.WriteLine("Enter project id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter name of the Employee :");
            emp.Name = Console.ReadLine();
            Employeemanager employeemanager = new Employeemanager();
            var valid = employeemanager.isvalidEmp(emp);
            if (!valid.isSucess)
            {
                projectmanager projectmanager = new projectmanager();
                var result = projectmanager.AddEmployeetoProject( emp,id);
                if (!result.isSucess)
                {
                    Console.WriteLine("Employee failed to add into project");
                    Console.WriteLine(result.status);
                }
                else
                {
                    Console.WriteLine(result.status);
                }
                return result.isSucess;
            }
            else
            {
                Console.WriteLine(valid.status);
            }
            return valid.isSucess;

        }



        private  static bool DeleteEmployeefromProject()
        {
            Employee emp = new Employee();
            Console.WriteLine("Enter project id:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter name of the Employee :");
            emp.Name = Console.ReadLine();
            Employeemanager employeemanager = new Employeemanager();
            var valid = employeemanager.isvalidEmp(emp);
            if (!valid.isSucess)
            {
                projectmanager projectmanager = new projectmanager();
                var result = projectmanager.DeleteEmployeefromProject(emp, id);
                if (!result.isSucess)
                {
                    Console.WriteLine("Employee failed to add into project");
                    Console.WriteLine(result.status);
                }
                else
                {
                    Console.WriteLine(result.status);
                }
                return result.isSucess;
            }
            else
            {
                Console.WriteLine(valid.status);
            }
            return valid.isSucess;
        }

        private bool AddRole()
        {
            Role rol = new Role();

            Console.WriteLine("Enter Role Id");
            rol.RoleId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter RoleName");
            rol.RoleName = Console.ReadLine();
            Rolemanager r = new Rolemanager();
            var res = r.AddRole(rol);
            if (!res.isSucess)
            {
                Console.WriteLine("Role already exists");
            }
            else
            {

            }
            return res.isSucess;
        }

        private bool AddEmployee()
        {

            Employee emp = new Employee();

            Console.WriteLine("Enter Employee Id");
            emp.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Employee Fullname");
            emp.Name = Console.ReadLine();
            Console.WriteLine("Enter Employee Contact");
            emp.Contact = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter Employee Email");
            emp.Email = Console.ReadLine();
            Employeemanager e = new Employeemanager();
            var res = e.AddEmployee(emp);
            if (!res.isSucess)
            {
                Console.WriteLine("employee already exists");
            }
            else
            {
                Console.WriteLine(res.status);
            }
            return res.isSucess;
        }


        private bool AddProject()
        {
            Project project = new Project();
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

            projectmanager p = new projectmanager();
            var res = p.AddProject(project);

            if (!res.isSucess)
            {
                Console.WriteLine("project fail to add");
            }
            else
            {

                Console.WriteLine(res.status);

            }
            return res.isSucess;
        }
    }
}
    


        
      
            
           
       
