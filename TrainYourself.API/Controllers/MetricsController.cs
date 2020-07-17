using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Repositories;

namespace TrainYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MetricsController : ControllerBase
    {
        private readonly IMongoRepository<Metric> _repo;

        public MetricsController(IMongoRepository<Metric> repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task AddMetric(MetricDto metric)
        {
            await _repo.InsertOneAsync(new Metric
            {
                Description = metric.Description,
                Name = metric.Name,
                IsActive = true,
                ModifiedAt = DateTime.Now
            });
        }

        [HttpPost]
        public async Task Update(MetricDto metric, string id)
        {
            await _repo.ReplaceOneAsync(new Metric
            {
                Name = metric.Name,
                Description = metric.Description,
                Id = new ObjectId(id),
                IsActive = true,
                ModifiedAt = DateTime.Now
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var metric = await _repo.FindByIdAsync(id);

            if (metric is null)
            {
                return NotFound($"Metric with id:({id}) not found.");
            }

            return Ok(metric);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var metrics = await _repo.GetAll();
            if (metrics == null || metrics.Any())
            {
                return NoContent();
            }

            return Ok(metrics);
        }

        [HttpDelete]
        public async Task Delete(string id)
        {
            await _repo.DeleteByIdAsync(id);
        }
    }
}