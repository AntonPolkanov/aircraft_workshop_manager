using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Awm.AwmDb;
using Awm.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Awm.Controllers
{
    [ApiController]
    [Route("api")]
    public class AircraftManagementController : ControllerBase
    {
        private awmContext dbcontext;
        private ILogger<AircraftManagementController> logger;

        public AircraftManagementController(awmContext dbcontext, ILogger<AircraftManagementController> logger)
        {
            this.dbcontext = dbcontext;
            this.logger = logger;
        }

        /// <summary>
        /// Get all aircrafts
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircrafts")]
        [Authorize]
        public async Task<IEnumerable<Aircraft>> GetAircraftList()
        {
            return await dbcontext.Aircraft.ToListAsync();
        }

        /// <summary>
        /// Get aircraft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("aircrafts/{id}")]
        [Authorize]
        public async Task<ActionResult<Aircraft>> GetAircraft(int id)
        {
            var aircraft = await dbcontext.Aircraft.FindAsync(id);
            if (aircraft == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Get aircraft by id {id}");
            return aircraft;
        }

        /// <summary>
        /// Create aircraft
        /// </summary>
        /// <param name="aircraft"></param>
        /// <returns></returns>
        [HttpPost("aircrafts")]
        [Authorize]
        public async Task<ActionResult<Aircraft>> CreateAircraft(Aircraft aircraft)
        {
            if (aircraft == null)
                return BadRequest();
            logger.Log(LogLevel.Debug, aircraft.AircraftId.ToString());
            await dbcontext.Aircraft.AddAsync(aircraft);
            await dbcontext.SaveChangesAsync();
            return CreatedAtAction("GetAircraft", new {id = aircraft.AircraftId}, aircraft);
        }

        /// <summary>
        /// Update aircraft (idempotent)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newAircraft"></param>
        /// <returns></returns>
        [HttpPut("aircrafts/{id}")]
        [Authorize]
        public async Task<ActionResult<Aircraft>> UpdateAircraft(int id, Aircraft newAircraft)
        {
            if (id != newAircraft.AircraftId)
                return BadRequest();
            logger.Log(LogLevel.Debug, $"Found aircraft {id}");
            dbcontext.Entry(newAircraft).State = EntityState.Modified;
            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!dbcontext.Aircraft.Any(a => a.AircraftId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await dbcontext.Aircraft.FindAsync(id);
        }
        
        /// <summary>
        /// Delete aircraft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("aircrafts/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAircraft(int id)
        {
            var aircraft = await dbcontext.Aircraft.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }

            dbcontext.Aircraft.Remove(aircraft);
            await dbcontext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Get all aircraft images
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/images")]
        [Authorize]
        public async Task<IEnumerable<AircraftImage>> GetAircraftImageList()
        {
            return await dbcontext.AircraftImage.ToListAsync();
        }

        /// <summary>
        /// Get aircraft image
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns>One image</returns>
        [HttpGet("aircraft/images/{imageId}")]
        [Authorize]
        public async Task<ActionResult<AircraftImage>> GetAircraftImage(int imageId)
        {
            var image = await dbcontext.AircraftImage.FindAsync(imageId);
            if (image == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Get aircraftImage by id {imageId}");
            return image;
        }

        /// <summary>
        /// Get all images related to an aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns>List of images</returns>
        [HttpGet("aircraft/{aircraftId}/images")]
        [Authorize]
        public ActionResult<IEnumerable<AircraftImage>> GetImagesOfAircraft(int aircraftId)
        {
            var aircraft = dbcontext.Aircraft
                .Where(a => a.AircraftId == aircraftId)
                .Include(a => a.AircraftImage)
                .FirstOrDefault();
                
            if (aircraft == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Found aircraft {aircraftId}");
            var result = new List<AircraftImageViewModel>();
            foreach (var image in aircraft.AircraftImage)
            {
                result.Add(
                    new AircraftImageViewModel
                    {
                        ImageId = image.ImageId,
                        DateTime = image.DateTime,
                        Comment = image.Comment,
                        S3Path = image.S3Path
                    });
            }
            return Ok(result);
        }

        /// <summary>
        /// Create aircraftImages for specified aircraft.
        /// If aircraft already has images, new images will be added
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <param name="aircraftImages"></param>
        /// <returns></returns>
        [HttpPost("aircraft/{aircraftId}/images/")]
        [Authorize]
        public async Task<ActionResult> CreateAircraftImages(int aircraftId, IEnumerable<AircraftImage> aircraftImages)
        {
            var aircraft = dbcontext.Aircraft
                .Where(a => a.AircraftId == aircraftId)
                .Include(a => a.AircraftImage)
                .FirstOrDefault();
            if (aircraft == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Found aircraft {aircraftId}");
            
            foreach (var image in aircraftImages)
            {
                await dbcontext.AircraftImage.AddAsync(image);
            }

            await dbcontext.SaveChangesAsync();
            return Ok();
        }
        
        /// <summary>
        /// Delete aircraft image
        /// </summary>
        /// <param name="aircraftImageId"></param>
        /// <returns></returns>
        [HttpDelete("aircraft/images/{aircraftImageId}")]
        [Authorize]
        public async Task<ActionResult> DeleteAircraftImage(int aircraftImageId)
        {
            var image = await dbcontext.AircraftImage.FindAsync(aircraftImageId);
            if (image == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Found aircraft image {aircraftImageId}");
            dbcontext.AircraftImage.Remove(image);
            await dbcontext.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Get all service timers
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/serviceTimers")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceTimer>> GetServiceTimerList()
        {
            var result = new List<ServiceTimerViewModel>();
            foreach (var timer in dbcontext.ServiceTimer)
            {
                result.Add(new ServiceTimerViewModel
                {
                    ServiceTimerId = timer.ServiceTimerId,
                    AircraftId = timer.AircraftId,
                    Status = timer.Status,
                    NextServiceDate = timer.NextServiceDate
                });
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all service timers related to an aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/{aircraftId}/serviceTimers")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceTimer>> GetServiceTimersOfAircraft(int aircraftId)
        {
            var aircraft = dbcontext.Aircraft
                .Where(a => a.AircraftId == aircraftId)
                .Include(a => a.ServiceTimer)
                .FirstOrDefault();
            if (aircraft == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Found aircraft image {aircraftId}");
            var result = new List<ServiceTimerViewModel>();
            foreach (var timer in aircraft.ServiceTimer)
            {
                result.Add(new ServiceTimerViewModel
                {
                    ServiceTimerId = timer.ServiceTimerId,
                    AircraftId = timer.AircraftId,
                    Status = timer.Status,
                    NextServiceDate = timer.NextServiceDate
                });
            }
            return Ok(result);
        }

        /// <summary>
        /// Create timer for specified aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <param name="timer"></param>
        /// <returns></returns>
        [HttpPost("aircraft/{aircraftId}/serviceTimers")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ServiceTimer>>> CreateServiceTimer(int aircraftId, ServiceTimer timer)
        {
            if (timer == null)
                return BadRequest();
            var aircraft = dbcontext.Aircraft
                .Where(a => a.AircraftId == aircraftId)
                .Include(a => a.ServiceTimer)
                .FirstOrDefault();
            if (aircraft == null)
                return NotFound();
            await dbcontext.ServiceTimer.AddAsync(timer);
            await dbcontext.SaveChangesAsync();
            var result = new ServiceTimerViewModel
            {
                ServiceTimerId = timer.ServiceTimerId,
                AircraftId = timer.AircraftId,
                Status = timer.Status,
                NextServiceDate = timer.NextServiceDate
            };
            return CreatedAtAction("GetServiceTimersOfAircraft", new {aircraftId = timer.AircraftId}, result);
        }

        /// <summary>
        /// Delete service timer
        /// </summary>
        /// <param name="timerId"></param>
        /// <returns></returns>
        [HttpDelete("aircraft/serviceTimers/{timerId}")]
        public async Task<ActionResult> DeleteServiceTimer(int timerId)
        {
            var timer = await dbcontext.ServiceTimer.FindAsync(timerId);
            if (timer == null)
                return NotFound();
            logger.Log(LogLevel.Debug, $"Found timer {timer.ServiceTimerId}");
            dbcontext.ServiceTimer.Remove(timer);
            await dbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}