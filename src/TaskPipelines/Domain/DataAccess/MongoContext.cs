using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TaskPipelines.Domain.ExecutableTasks;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Domain.DataAccess
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("Mongo"));
            _database = client.GetDatabase("tasks");
        }

        public IMongoCollection<ExecutableTask> Tasks => _database.GetCollection<ExecutableTask>("ExecutableTask");

        public IMongoCollection<Pipeline> Pipelines => _database.GetCollection<Pipeline>("Pipelines");
    }
}