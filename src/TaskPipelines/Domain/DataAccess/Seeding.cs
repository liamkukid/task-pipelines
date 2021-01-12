using MongoDB.Driver;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Domain.DataAccess
{
    public static class Seeding
    {
        public static void AddPipelines(MongoContext context)
        {
            if (!context.Pipelines.Find(_ => true).Any())
            {
                context.Pipelines.InsertOne(new Pipeline());
            }
        }
    }
}