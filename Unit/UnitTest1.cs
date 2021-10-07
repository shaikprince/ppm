using System;
using Domain;
using Model;
using NUnit.Framework;

namespace Unit
{
    public class Tests
    {

        [Test]
        public void AddprojectTest1()
        {
            Projectmanager pro = new Projectmanager();
            Project pp = new Project();
            pp.id = 1;
            pp.Name = "Eshu";
            pp.StartDate = Convert.ToDateTime("1-2-2021");
            pp.EndDate = Convert.ToDateTime("2-2-2021");
            pp.Budget = 2000;
            var v2 = pro.Add(pp);
            if (v2.isSucess)
            {
                Assert.Pass();

            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void AddEmployeeTest1()
        {
            Employeemanager emp = new Employeemanager();
            Employee ee = new Employee();
            ee.Id = 1;
            ee.Name = "Prince";
            ee.Contact = 1245799326;
            ee.Email = "shaikprince@";
            var v2 = emp.Add(ee);
            if (v2.isSucess)
            {
                Assert.Pass();

            }
            else
            {
                Assert.Fail();
            }
        }
        [Test]
        public void AddRoleTest1()
        {
            Rolemanager role = new Rolemanager();
            Role rr = new Role();
            rr.RoleId = 1;
            rr.RoleName = "Hr";
            var v2 = role.Add(rr);
            if (v2.isSucess)
            {
                Assert.Pass();

            }
            else
            {
                Assert.Fail();
            }

        }
        [Test]
        public void AddEmployeeToProjectTest1()
        {
            Employee emp = new Employee();
            Employeemanager ee = new Employeemanager();
            int projectId = 1;
            emp.Name = "Eshu";
            var v1 = ee.isvalidEmp(emp);
            if (!v1.isSucess)
            {
                Projectmanager projectmanager = new Projectmanager();
                var r1 = projectmanager.AddEmployeetoProject(emp, projectId);
                if (!r1.isSucess)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }

            }
            


        }
        [Test]
        public void DeleteEmployeefromProjectTest1()
        {
            Employee emp = new Employee();
            Employeemanager ee = new Employeemanager();
            int projectId = 1;
            emp.Name = "Eshu";
            var v1 = ee.isvalidEmp(emp);
            if (!v1.isSucess)
            {
                Projectmanager projectmanager = new Projectmanager();
                var r1 = projectmanager.DeleteEmployeefromProject(emp, projectId);
                if (!r1.isSucess)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }

            }
        }


    }
}

    
