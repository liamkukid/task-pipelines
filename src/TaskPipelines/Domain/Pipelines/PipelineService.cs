using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TaskPipelines.Domain.DataAccess;
using TaskPipelines.Domain.Exceptions;

namespace TaskPipelines.Domain.Pipelines
{
    public class PipelineService
    {
        private readonly MongoContext _context;

        public PipelineService(MongoContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<PipelineResponse>> StartedPipelinesAsync()
        {
            var pipelines = await _context.Pipelines
                .Find(x => x.FinishedAt == null && x.StartedAt != null)
                .ToListAsync();

            var result = new List<PipelineResponse>();

            foreach (Pipeline pipeline in pipelines)
            {
                result.Add(await FindTasksForPipelineAsync(pipeline));
            }

            return result;
        }

        public async Task<IReadOnlyCollection<PipelineResponse>> AllAsync()
        {
            List<Pipeline> pipelines = await _context.Pipelines.Find(_ => true).ToListAsync();

            var result = new List<PipelineResponse>();

            foreach (Pipeline pipeline in pipelines)
            {
                result.Add(await FindTasksForPipelineAsync(pipeline));
            }

            return result;
        }

        private async Task<PipelineResponse> FindTasksForPipelineAsync(Pipeline pipeline)
        {
            var tasks = await (await _context.Tasks
                    .FindAsync(x => x.PipelineId == pipeline.Id))
                .ToListAsync();

            return new PipelineResponse(
                pipeline: pipeline,
                tasks: tasks);
        }

        public async Task<PipelineResponse> GetAsync(string id)
        {
            var pipeline = await _context.Pipelines.ByIdOrNullAsync(id)
                           ?? throw ResourceNotFoundException.FromEntity<Pipeline>(id);

            var tasks = await _context.Tasks
                .Find(x => x.PipelineId == pipeline.Id)
                .ToListAsync();

            return new PipelineResponse(pipeline, tasks);
        }

        public async Task<string> CreateAsync()
        {
            var pipeline = new Pipeline();
            await _context.Pipelines.InsertOneAsync(pipeline);
            return pipeline.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var tasksDeletion = await _context.Tasks.DeleteManyAsync(x => x.PipelineId == id);
            var pipelineDeletion = await _context.Pipelines.DeleteOneAsync(x => x.Id == id);
        }

        public async Task StartAsync(string id)
        {
            PipelineResponse pipeline = await GetAsync(id);

            if (!pipeline.Tasks.Any())
            {
                throw new InvalidOperationException();
            }

            pipeline.Pipeline.Start();
        }
    }
}