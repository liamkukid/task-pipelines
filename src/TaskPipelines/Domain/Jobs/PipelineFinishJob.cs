using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Domain.Jobs
{
    public class PipelineFinishJob : BaseJob
    {
        private readonly PipelineService _service;

        public PipelineFinishJob(ILogger<PipelineFinishJob> logger, PipelineService service)
            : base(logger)
        {
            _service = service;
        }

        protected override async Task InvokeAsync()
        {
            var pipelines = (await _service.StartedPipelinesAsync())
                .Where(x => x.Pipeline.CouldBeFinished())
                .ToArray();

            foreach (PipelineResponse pipelineResponse in pipelines)
            {
                pipelineResponse.Pipeline.Finish();
            }
        }
    }
}