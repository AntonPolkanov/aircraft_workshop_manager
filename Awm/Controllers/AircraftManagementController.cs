using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Awm.AwmDb;
using Awm.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return Ok(aircraft);
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
            if (aircraft.AircraftId == 0) // default
            {
                var maxId = await dbcontext.Aircraft.MaxAsync(a => a.AircraftId);
                aircraft.AircraftId = maxId + 1;
            }
            
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

            return Ok(image);
        }

        /// <summary>
        /// Get all images related to aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns>List of images</returns>
        [HttpGet("aircraft/{aircraftId}/images")]
        [Authorize]
        public ActionResult<IEnumerable<AircraftImageViewModel>> GetImagesOfAircraftList(int aircraftId)
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
        public ActionResult<IEnumerable<ServiceTimerViewModel>> GetServiceTimerList()
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
        /// Get service timer
        /// </summary>
        /// <param name="serviceTimerId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/serviceTimers/{serviceTimerId}")]
        [Authorize]
        public async Task<ActionResult<ServiceTimerViewModel>> GetServiceTimer(int serviceTimerId)
        {
            var timer = await dbcontext.ServiceTimer.FindAsync(serviceTimerId);
            if (timer == null)
                return NotFound();
            var result = new ServiceTimerViewModel
            {
                ServiceTimerId = timer.ServiceTimerId,
                AircraftId = timer.AircraftId,
                Status = timer.Status,
                NextServiceDate = timer.NextServiceDate
            };
            return Ok(result);
        }

        /// <summary>
        /// Get all service timers related to aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/{aircraftId}/serviceTimers")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceTimerViewModel>> GetServiceTimersOfAircraftList(int aircraftId)
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
        public async Task<ActionResult<IEnumerable<ServiceTimerViewModel>>> CreateServiceTimer(int aircraftId,
            ServiceTimer timer)
        {
            if (timer == null)
                return BadRequest();
            if (timer.ServiceTimerId == 0) // default
            {
                var maxId = await dbcontext.ServiceTimer.MaxAsync(t => t.ServiceTimerId);
                timer.ServiceTimerId = maxId + 1;
            }
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

            return CreatedAtAction("GetServiceTimersOfAircraftList", new {aircraftId = timer.AircraftId}, result);
        }

        /// <summary>
        /// Delete service timer
        /// </summary>
        /// <param name="timerId"></param>
        /// <returns></returns>
        [HttpDelete("aircraft/serviceTimers/{timerId}")]
        [Authorize]
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

        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/services")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceViewModel>> GetServicesList()
        {
            var result = new List<ServiceViewModel>();
            foreach (var service in dbcontext.Service)
            {
                result.Add(new ServiceViewModel
                {
                    ServiceId = service.ServiceId,
                    AircraftId = service.AircraftId,
                    Date = service.Date,
                    Description = service.Description,
                    Name = service.Name,
                    ClientQuotesHrs = service.ClientQuotesHrs
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/{serviceId}")]
        [Authorize]
        public async Task<ActionResult<ServiceViewModel>> GetService(int serviceId)
        {
            var service = await dbcontext.Service.FindAsync(serviceId);
            if (service == null)
                return NotFound();
            var result = new ServiceViewModel
            {
                ServiceId = service.ServiceId,
                AircraftId = service.AircraftId,
                Date = service.Date,
                Description = service.Description,
                Name = service.Name,
                ClientQuotesHrs = service.ClientQuotesHrs
            };

            return Ok(result);
        }

        /// <summary>
        /// Get services of specified aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/{aircraftId}/services")]
        [Authorize]
        public ActionResult<IEnumerable<ServiceViewModel>> GetServicesOfAircraftList(int aircraftId)
        {
            var aircraft = dbcontext.Aircraft
                .Where(a => a.AircraftId == aircraftId)
                .Include(a => a.Service)
                .FirstOrDefault();
            if (aircraft == null)
                return NotFound();
            var result = new List<ServiceViewModel>();
            foreach (var service in aircraft.Service)
            {
                result.Add(new ServiceViewModel
                {
                    ServiceId = service.ServiceId,
                    AircraftId = service.AircraftId,
                    Date = service.Date,
                    Description = service.Description,
                    Name = service.Name,
                    ClientQuotesHrs = service.ClientQuotesHrs
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Create service for specified aircraft
        /// </summary>
        /// <param name="aircraftId"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        [HttpPost("aircraft/{aircraftId}/services")]
        [Authorize]
        public async Task<ActionResult<ServiceViewModel>> CreateService(int aircraftId, Service service)
        {
            if (aircraftId != service.AircraftId)
                return BadRequest();
            var aircraft = await dbcontext.Aircraft.FindAsync(aircraftId);
            if (aircraft == null)
                return NotFound();
            await dbcontext.Service.AddAsync(service);
            await dbcontext.SaveChangesAsync();
            var result = new ServiceViewModel
            {
                ServiceId = service.ServiceId,
                AircraftId = service.AircraftId,
                Date = service.Date,
                Description = service.Description,
                Name = service.Name,
                ClientQuotesHrs = service.ClientQuotesHrs
            };

            return CreatedAtAction("GetService", new {serviceId = service.ServiceId}, result);
        }

        /// <summary>
        /// Delete service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpDelete("aircraft/services/{serviceId}")]
        [Authorize]
        public async Task<ActionResult<ServiceViewModel>> DeleteService(int serviceId)
        {
            var service = await dbcontext.Service.FindAsync(serviceId);
            if (service == null)
                return NotFound();
            dbcontext.Service.Remove(service);
            await dbcontext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Get all jobs
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs")]
        [Authorize]
        public ActionResult<IEnumerable<JobViewModel>> GetJobsList()
        {
            var result = new List<JobViewModel>();
            foreach (var job in dbcontext.Job)
            {
                result.Add(new JobViewModel
                {
                    JobId = job.JobId,
                    EmailAddressId = job.EmailAddressId,
                    Status = job.Status,
                    AllocatedHours = job.AllocatedHours,
                    CumulativeHours = job.CumulativeHours,
                    JobDescription = job.JobDescription
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Get job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/{jobId}")]
        [Authorize]
        public async Task<ActionResult<JobViewModel>> GetJob(int jobId)
        {
            var job = await dbcontext.Job.FindAsync(jobId);
            if (job == null)
                return NotFound();
            var result = new JobViewModel
            {
                JobId = job.JobId,
                EmailAddressId = job.EmailAddressId,
                Status = job.Status,
                AllocatedHours = job.AllocatedHours,
                CumulativeHours = job.CumulativeHours,
                JobDescription = job.JobDescription
            };

            return Ok(result);
        }

        /// <summary>
        /// Get all jobs related to service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/{serviceId}/jobs")]
        [Authorize]
        public ActionResult<IEnumerable<JobViewModel>> GetJobsOfServiceList(int serviceId)
        {
            var service = dbcontext.Service
                .Where(s => s.ServiceId == serviceId)
                .Include(s => s.Job)
                .FirstOrDefault();
            if (service == null)
                return NotFound();
            var result = new List<JobViewModel>();
            foreach (var job in service.Job)
            {
                result.Add(new JobViewModel
                {
                    JobId = job.JobId,
                    EmailAddressId = job.EmailAddressId,
                    Status = job.Status,
                    AllocatedHours = job.AllocatedHours,
                    CumulativeHours = job.CumulativeHours,
                    JobDescription = job.JobDescription
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Create job for specified service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        [HttpPost("aircraft/services/{serviceId}/jobs")]
        [Authorize]
        public async Task<ActionResult<JobViewModel>> CreateJob(int serviceId, Job job)
        {
            if (serviceId != job.JobId)
                return BadRequest();
            var service = await dbcontext.Service.FindAsync(serviceId);
            if (service == null)
                return NotFound();
            await dbcontext.Job.AddAsync(job);
            await dbcontext.SaveChangesAsync();
            var result = new JobViewModel
            {
                JobId = job.JobId,
                EmailAddressId = job.EmailAddressId,
                Status = job.Status,
                AllocatedHours = job.AllocatedHours,
                CumulativeHours = job.CumulativeHours,
                JobDescription = job.JobDescription
            };
            
            return CreatedAtAction("GetJob", new {jobId = job.JobId}, result);
        }

        /// <summary>
        /// Get all spare parts
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/spareParts")]
        [Authorize]
        public ActionResult<SparePartViewModel> GetSparePartsList()
        {
            var result = new List<SparePartViewModel>();
            foreach (var part in dbcontext.SparePart)
            {
                result.Add(new SparePartViewModel
                {
                    PartId = part.PartId,
                    JobId = part.JobId,
                    Gnr = part.Gnr,
                    IntakeDate = part.IntakeDate,
                    BestBeforeDate = part.BestBeforeDate
                });
            }

            return Ok(result);
        }
        
        /// <summary>
        /// Get spare part
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/spareParts/{partId}")]
        [Authorize]
        public async Task<ActionResult<SparePartViewModel>> GetSparePart(int partId)
        {
            var part = await dbcontext.SparePart.FindAsync(partId);
            if (part == null)
                return NotFound();
            var result = new SparePartViewModel
            {
                PartId = part.PartId,
                JobId = part.JobId,
                Gnr = part.Gnr,
                IntakeDate = part.IntakeDate,
                BestBeforeDate = part.BestBeforeDate
            };

            return Ok(result);
        }

        /// <summary>
        /// Get spare parts related to job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/{jobId}/spareParts")]
        [Authorize]
        public ActionResult<IEnumerable<SparePartViewModel>> GetSparePartsOfJobList(int jobId)
        {
            var job = dbcontext.Job
                .Where(j => j.JobId == jobId)
                .Include(j => j.SparePart)
                .FirstOrDefault();
            if (job == null)
                return NotFound();
            var result = new List<SparePartViewModel>();
            foreach (var part in job.SparePart)
            {
                result.Add(new SparePartViewModel
                {
                    PartId = part.PartId,
                    JobId = part.JobId,
                    Gnr = part.Gnr,
                    IntakeDate = part.IntakeDate,
                    BestBeforeDate = part.BestBeforeDate
                });
            }

            return Ok(result);
        }

        /// <summary>
        /// Create spare part for specified job
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        [HttpPost("aircraft/services/jobs/{jobId}/spareParts")]
        [Authorize]
        public async Task<ActionResult<SparePartViewModel>> CreateSparePart(int jobId, SparePart part)
        {
            var job = await dbcontext.Job.FindAsync(jobId);
            if (job == null)
                return NotFound();
            await dbcontext.SparePart.AddAsync(part);
            await dbcontext.SaveChangesAsync();
            var result = new SparePartViewModel
            {
                PartId = part.PartId,
                JobId = part.JobId,
                Gnr = part.Gnr,
                IntakeDate = part.IntakeDate,
                BestBeforeDate = part.BestBeforeDate
            };

            return CreatedAtAction("GetSparePart", new {partId = part.PartId}, result);
        }
        
        /// <summary>
        /// Get all materials
        /// </summary>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/materials")]
        [Authorize]
        public ActionResult<MaterialViewModel> GetMaterialList()
        {
            var result = new List<MaterialViewModel>();
            foreach (var material in dbcontext.Material)
            {
                result.Add(new MaterialViewModel
                {
                    MaterialId = material.MaterialId,
                    JobId = material.JobId,
                    Gnr = material.Gnr,
                    IntakeDate = material.IntakeDate,
                    BestBeforeDate = material.BestBeforeDate
                });
            }

            return Ok(result);
        }
        
        /// <summary>
        /// Get material
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/materials/{materialId}")]
        [Authorize]
        public async Task<ActionResult<MaterialViewModel>> GetMaterial(int materialId)
        {
            var material = await dbcontext.Material.FindAsync(materialId);
            if (material == null)
                return NotFound();
            var result = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                JobId = material.JobId,
                Gnr = material.Gnr,
                IntakeDate = material.IntakeDate,
                BestBeforeDate = material.BestBeforeDate
            };

            return Ok(result);
        }
        
        /// <summary>
        /// Get materials related to job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("aircraft/services/jobs/{jobId}/materials")]
        [Authorize]
        public ActionResult<IEnumerable<MaterialViewModel>> GetMaterialsOfJobList(int jobId)
        {
            var job = dbcontext.Job
                .Where(j => j.JobId == jobId)
                .Include(j => j.Material)
                .FirstOrDefault();
            if (job == null)
                return NotFound();
            var result = new List<MaterialViewModel>();
            foreach (var material in job.Material)
            {
                result.Add(new MaterialViewModel
                {
                    MaterialId = material.MaterialId,
                    JobId = material.JobId,
                    Gnr = material.Gnr,
                    IntakeDate = material.IntakeDate,
                    BestBeforeDate = material.BestBeforeDate
                });
            }

            return Ok(result);
        }
        
        /// <summary>
        /// Create material for specified job
        /// </summary>
        /// <param name="jobId"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPost("aircraft/services/jobs/{jobId}/materials")]
        [Authorize]
        public async Task<ActionResult<MaterialViewModel>> CreateMaterial(int jobId, Material material)
        {
            var job = await dbcontext.Job.FindAsync(jobId);
            if (job == null)
                return NotFound();
            await dbcontext.Material.AddAsync(material);
            await dbcontext.SaveChangesAsync();
            var result = new MaterialViewModel
            {
                MaterialId = material.MaterialId,
                JobId = material.JobId,
                Gnr = material.Gnr,
                IntakeDate = material.IntakeDate,
                BestBeforeDate = material.BestBeforeDate
            };

            return CreatedAtAction("GetMaterial", new {materialId = material.MaterialId}, result);
        }
    }
}