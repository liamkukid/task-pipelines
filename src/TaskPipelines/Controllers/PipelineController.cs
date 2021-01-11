﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskPipelines.Domain.Pipelines;

namespace TaskPipelines.Controllers
{
    [ApiController]
    [Route("api/pipelines")]
    [AllowAnonymous]
    public class PipelineController : ControllerBase
    {
        private readonly PipelineService _service;

        public PipelineController(PipelineService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public Task<IReadOnlyCollection<Pipeline>> AllAsync() => _service.AllAsync();

        [HttpGet("{id}")]
        public Task<PipelineResponse> GetAsync([FromQuery] long id) => _service.GetAsync(id);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromQuery] long id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}