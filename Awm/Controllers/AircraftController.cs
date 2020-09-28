using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Awm.AwmDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Awm.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AircraftController : ControllerBase
    {
        private awmContext dbcontext;

        public AircraftController(awmContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Aircraft>> Get()
        {
            return await dbcontext.Aircraft.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Aircraft>> Get(int id)
        {
            var aircraft = await dbcontext.Aircraft.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return aircraft;
        }

        [HttpPost]
        public async Task<ActionResult<Aircraft>> Post(Aircraft aircraft)
        {
            await dbcontext.Aircraft.AddAsync(aircraft);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction("Get", new {id = aircraft.AircraftId}, aircraft);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Aircraft>> Delete(int id)
        {
            var aircraft = await dbcontext.Aircraft.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            dbcontext.Aircraft.Remove(aircraft);
            await dbcontext.SaveChangesAsync();
            return aircraft;
        }
    }
}