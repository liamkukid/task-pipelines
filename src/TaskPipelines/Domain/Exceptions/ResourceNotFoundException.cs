using System;

namespace TaskPipelines.Domain.Exceptions
{
    public class ResourceNotFoundException : InvalidOperationException
    {
        private ResourceNotFoundException(string message)
            : base(message)
        {
        }

        public static ResourceNotFoundException FromEntity<T>(long id)
        {
            return new ResourceNotFoundException($"Cannot find entity of type {typeof(T).Name} with id:{id}");
        }
    }
}