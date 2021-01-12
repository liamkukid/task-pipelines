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
        public async Task<IReadOnlyCollection<ExecutableTask>> OfPipelineAsync(string pipelineId)
        {
            return await _context.Tasks.Find(x => x.PipelineId == pipelineId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ExecutableTask> GetAsync(string id)
        {
            return await _context.Tasks.ByIdOrNullAsync(id)
                ?? throw ResourceNotFoundException.FromEntity<ExecutableTask>(id);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] ExecutableTaskCreateRequest request)
        {
            ExecutableTask task;
            if (request.PreviousTaskId != null)
            {
                var previousTask = await _context.Tasks.ByIdOrNullAsync(request.PreviousTaskId);
                task = new ExecutableTask(request.Name, previousTask);
                
            }
            else if (request.PipelineId != null)
            {
                var pipeline = await _context.Pipelines.ByIdOrNullAsync(request.PipelineId);
                task = new ExecutableTask(request.Name, pipeline);
            }
            else
            {
                return new BadRequestResult();
            }

            await _context.Tasks.InsertOneAsync(task);
            return Ok(task.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _context.Tasks.DeleteOneAsync(x => x.Id == id);
            return Ok();
        }
    }
}