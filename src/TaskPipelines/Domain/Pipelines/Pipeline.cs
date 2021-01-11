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
    }
}