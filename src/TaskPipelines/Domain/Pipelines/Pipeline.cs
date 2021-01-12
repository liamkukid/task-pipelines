using System;
using TaskPipelines.Domain.DataAccess;

namespace TaskPipelines.Domain.Pipelines
{
    public class Pipeline : BaseModel
    {
        public Pipeline()
        {
            CreatedAt = UpdatedAt = DateTime.Now;
        }

        public bool Launched => StartedAt.HasValue;

        public bool Finished => FinishedAt.HasValue;

        public DateTime? StartedAt { get; protected set; }

        public DateTime? FinishedAt { get; protected set; }

        public void Start()
        {
            if (Launched || Finished)
            {
                throw new InvalidOperationException();
            }

            StartedAt = DateTime.Now;
        }

        public void Finish()
        {
            if (!Launched || Finished)
            {
                throw new InvalidOperationException();
            }

            FinishedAt = DateTime.Now;
        }

        public bool CouldBeFinished()
        {
            if (Launched || Finished)
            {
                throw new InvalidOperationException();
            }

            return DateTime.Now >= StartedAt;
        }
    }
}