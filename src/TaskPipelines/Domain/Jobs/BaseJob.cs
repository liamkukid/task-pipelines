using System;
using System.Threading.Tasks;
using Coravel.Invocable;
using Microsoft.Extensions.Logging;

namespace TaskPipelines.Domain.Jobs
{
    public abstract class BaseJob : IInvocable
    {
        private readonly ILogger _logger;

        protected BaseJob(ILogger logger)
        {
            _logger = logger;
        }

        protected abstract Task InvokeAsync();

        public async Task Invoke()
        {
            try
            {
                await InvokeAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(1, e, $"Exception during {GetType().Name}");
            }
        }
    }
}