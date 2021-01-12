using System;
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

        public bool CouldBeFinished()
        {
            if (Pipeline.StartedAt == null || Pipeline.Finished)
            {
                throw new InvalidOperationException();
            }

            var date = DateTimeOffset.FromFileTime(Pipeline.StartedAt.Value.ToFileTime());
            var shouldFinishAfter = date.Add(TimeSpan.FromMilliseconds(Duration));

            var result = DateTime.Compare(DateTime.Now, shouldFinishAfter.DateTime) > 0;

            return result;
        }
    }
}