using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapiv1.Interfaces;

namespace webapiv1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly Assembly _assembly;
        private readonly IServiceProvider _provider;

        public JobController(IServiceProvider provider)
        {
            _assembly = typeof(JobController).Assembly;
            _provider = provider;
        }

        [HttpGet]
        public ActionResult<string> Get(string name)
        {
            try 
            {
                var fullName = $"webapiv1.Jobs.{name}";
                var service = _provider.GetService(_assembly.GetType(fullName)) as IJob;
                var jobId = BackgroundJob.Enqueue(() => service.Execute());
                return Ok(jobId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}