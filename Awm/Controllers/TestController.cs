using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Awm.Controllers
{
    /// <summary>
    /// Unprotected endpoint for test purposes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Test endpoint
        /// </summary>
        /// <returns>Dummy data</returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Hello from Baijiu team!");
        }
        
    }
}