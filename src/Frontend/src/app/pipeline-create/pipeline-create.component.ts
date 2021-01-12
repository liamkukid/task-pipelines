import { Component, OnInit } from '@angular/core';
import { ExecutableTask } from '../models/executable-task';
import { Pipeline } from '../models/pipeline';
import { PipelineResponse } from '../models/pipeline-response';
import { ExecutableTasksService } from '../services/executable-tasks.service';
import { PipelinesService } from '../services/pipelines.service';

@Component({
  selector: 'app-pipeline-create',
  templateUrl: './pipeline-create.component.html'
})
export class PipelineCreateComponent implements OnInit {

  pipeline: PipelineResponse;

  constructor(
    private readonly pipelines: PipelinesService,
    private readonly tasks: ExecutableTasksService) {}

  ngOnInit(): void {
    this.pipelines.all().subscribe(x => {
      this.pipeline = x[0];
    });
  }

  addTask(): void {
    const name = Date.now().toString();
    this.tasks.create({ name: name, pipelineId: null, previousTaskId: null } as ExecutableTask).subscribe(() => {
      this.ngOnInit();
    });
  }
}
