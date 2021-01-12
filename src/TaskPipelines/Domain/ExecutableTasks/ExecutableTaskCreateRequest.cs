using System.ComponentModel.DataAnnotations;

namespace TaskPipelines.Domain.ExecutableTasks
{
    public class ExecutableTaskCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string? PreviousTaskId { get; set; }

        public string? PipelineId { get; set; }
    }
}