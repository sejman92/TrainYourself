using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainYourself.API.Dtos;
using TrainYourself.API.Models;
using TrainYourself.API.Repositories;

namespace TrainYourself.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MetricController : ControllerBase
    {
        private readonly IMongoRepository<Metric> _repo;

        public MetricController(IMongoRepository<Metric> repo)
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
                IsActive = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var metric = await _repo.FindByIdAsync(id);

            if (metric is null)
            {
                return NotFound($"Metric with id:({id}) not found.");
            }

            return Ok(metric);
        }
    }
}