using System.Collections.Generic;
using System.Linq;
using TaskPipelines.Domain.ExecutableTasks;

namespace TaskPipelines.Domain.Pipelines
{
    public class PipelineResponse
    {
        public PipelineResponse(Pipeline pipeline, IReadOnlyCollection<ExecutableTask> tasks)
        {
            Pipeline = pipeline;
            Tasks = tasks;
        }

        public Pipeline Pipeline { get; }

        public IReadOnlyCollection<ExecutableTask> Tasks { get; }

        public int Duration => Tasks.Sum(x => x.Duration);
    }
}