using System;
using System.Collections.Generic;



namespace ppm
{
    class Program

    {
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long Contact { get; set; }
            public string Email { get; set; }
        }
        public class Project
        {
            public int id { get; set; }
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal Budget { get; set; }
        }
        public class Role
        {
            public int RoleId { get; set; }
            public string RoleName { get; set; }
        }




        static void Main(string[] args)
        {
            Console.WriteLine("following operation");
            Console.WriteLine("1.Add project");
            Console.WriteLine("2.View project");
            Console.WriteLine("3.Add Employee");
            Console.WriteLine("4.View Employee");
            Console.WriteLine("5.Add Role");
            Console.WriteLine("6.View Role");
            Console.WriteLine("7. Quit");

            List<Employee> e=  new List<Employee>();
            List<Project> p1 = new List<Project>();
            List<Role> r1 = new List<Role>();

            bool i = true;
            while (i)
            {
                Console.Write("select from any option: ");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {

                    case 1:
                      Project p2 = new Project();
                        
                        Console.WriteLine("Enter Project id");
                        p2.id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Project Name");
                        p2.Name = Console.ReadLine();
                        Console.WriteLine("Enter Project Start Date");
                        p2.StartDate =  Convert.ToDateTime (Console.ReadLine());
                        Console.WriteLine("Enter Project End Date");
                       p2. EndDate =  Convert.ToDateTime (Console.ReadLine());
                        Console.WriteLine("Enter Project Budget");
                        p2.Budget = Convert.ToDecimal(Console.ReadLine());

                        if (p1.Exists(proj => proj.id == p2.id))
                        {
                            Console.WriteLine("project already exists");
                        }
                        else
                        {
                           p1.Add(p2);
                        }

                        break;
                    case 2:
                        int c = 0;
                        foreach (Project p in p1)
                        {
                            Console.WriteLine("Project " + c);
                            Console.WriteLine("projectId:" +p.id); 
                            Console.WriteLine("projectName:"+p.Name);
                            Console.WriteLine("projectStartDate:"+p.StartDate);
                            Console.WriteLine("projectEndDate:"+p.EndDate);
                            Console.WriteLine("projectBudget:"+p.Budget);
                            c++;
                        }
                        break;
                    case 3:
                        Employee emp = new Employee();
                        
                        Console.WriteLine("Enter Employee Id");
                        emp.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Employee Fullname");
                        emp.Name = Console.ReadLine();
                        Console.WriteLine("Enter Employee Contact");
                        emp.Contact = Convert.ToInt64(Console.ReadLine());
                        Console.WriteLine("Enter Employee Email");
                        emp.Email = Console.ReadLine();


                        if (e.Exists(e1 => e1.Id == emp.Id))
                        {
                            Console.WriteLine("employee already exists");
                        }
                        else
                        {
                           e.Add(emp);
                        }
                        break;
                    case 4:
                        int j = 0;
                        foreach (Employee item in e)
                        {

                            Console.WriteLine("Employee no " + i);
                            Console.WriteLine("Employee id:"+item.Id);
                            Console.WriteLine("Employee Name:"+item.Name);
                            Console.WriteLine("Employee Contact:"+item.Contact);
                            Console.WriteLine("Employee Email:"+item.Email);



                            j++;
                        }
                        break;
                    case 5:
                        string[] role = new string[2];
                        Role rol = new Role();
                       
                        Console.WriteLine("Enter Role Id");
                        rol.RoleId =  Convert.ToInt32( Console.ReadLine());
                        Console.WriteLine("Enter RoleName");
                       rol.RoleName =  Console.ReadLine();
                        if (r1.Exists(r2 => r2.RoleId == rol.RoleId))
                        {
                            Console.WriteLine("Role already exists");
                        }
                        else
                        {
                            r1.Add(rol);
                        }



                        break;
                    case 6:
                        int count = 0;
                        foreach ( Role r in r1)
                        {
                            Console.WriteLine("Role no " + count);



                            Console.WriteLine("Role id:"+r.RoleId);
                            Console.WriteLine("Role Name:"+r.RoleName);

                            count++;
                        }
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;



                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }



        }
    }
}

