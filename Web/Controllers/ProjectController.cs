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
    public class ProjectController : ControllerBase
    {
        public IActionResult AddProject(Project project)
        {
            if (project == null)
            {
                return NotFound();
            }
            Projectmanager projectManager = new();
            var addProject = projectManager.Add(project);
            if (addProject.isSucess)
            {
                return Ok();
            }
            return ValidationProblem();
        }
    }
    }

