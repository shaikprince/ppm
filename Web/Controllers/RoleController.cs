using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public IActionResult AddRole(Role role)
        {
            if(role == null)
            {
                return NotFound();
            }
            Rolemanager roleManager = new();
            var addRole = roleManager.Add(role);
            if (addRole.isSucess)
            {
                return Ok();
            }
            return ValidationProblem();
        }
        // public DataResult<Role> ListAll()
        //public void DeleteRoleById()
    }

}
