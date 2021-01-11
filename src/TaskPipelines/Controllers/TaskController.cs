using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TaskPipelines.Domain;
using TaskPipelines.Domain.DataAccess;
using TaskPipelines.Domain.Exceptions;
using TaskPipelines.Domain.ExecutableTasks;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [AllowAnonymous]
    public class TaskController : ControllerBase
    {
        private readonly MongoContext _context;
        private readonly PipelineService _pipelines;

        public TaskController(MongoContext context, PipelineService pipelines)
        {
            _context = context;
            _pipelines = pipelines;
        }

        [HttpGet("")]
        public async Task<IReadOnlyCollection<ExecutableTask>> AllAsync()
        {
            return await _context.Tasks.Find(_ => true).ToListAsync();
        }

        [HttpGet("pipeline/{pipelineId}")]
        public async Task<IReadOnlyCollection<ExecutableTask>> OfPipelineAsync([FromQuery] long pipelineId)
        {
            return await _context.Tasks.Find(x => x.PipelineId == pipelineId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ExecutableTask> GetAsync([FromQuery] long id)
        {
            return await _context.Tasks.ByIdOrNullAsync(id)
                ?? throw ResourceNotFoundException.FromEntity<ExecutableTask>(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] long id)
        {
            await _context.Tasks.DeleteOneAsync(x => x.Id == id);
            return Ok();
        }
    }
}