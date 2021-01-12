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

  get showStartButton(): boolean {
    return this.pipeline != null &&
           this.pipeline.tasks.length > 0 &&
           this.pipeline.pipeline.startedAt == null;
  }

  get showDeletetButton(): boolean {
    return this.pipeline != null &&
           this.pipeline.pipeline.finishedAt != null;
  }

  private intervalId: any | null = null;

  constructor(
    private readonly pipelines: PipelinesService,
    private readonly tasks: ExecutableTasksService) {}

  ngOnInit(): void {
    this.pipelines.all().subscribe(x => {

      if (x.length > 0) {
        this.pipeline = x[0];
      } else {
        this.pipeline = null;
      }
      
    });
  }

  createPipeline(): void {
    if (this.pipeline != null) {
      throw Error('There is a created pipeline');
    }

    this.pipelines.create().subscribe((resp) => {
      this.getPipeline(resp.id);
    });
  }

  private getPipeline(id: string): void {
    this.pipelines.get(id).subscribe(x => {
      this.pipeline = x;
    });
  }

  deletePipeline(): void {
    if (this.pipeline == null) {
      throw Error('There is no created pipelines');
    }

    this.pipelines.delete(this.pipeline.pipeline.id).subscribe(() => {
      this.ngOnInit();
    });
  }

  startPipeline(): void {
    if (this.pipeline.tasks.length > 0) {
      this.pipelines.start(this.pipeline.pipeline.id).subscribe(() => {
        console.log('started');
        const id = this.pipeline.pipeline.id;
        this.intervalId = setInterval(() => {
          this.pipelines.get(id).subscribe(x => {
            this.pipeline = x;

            if (this.pipeline.pipeline.finishedAt != null) {
              clearInterval(this.intervalId);
            }
          });
        }, 200);
      });
    } else {
      console.log('No tasks to start');
    }
  }

  addTask(): void {
    const name = Date.now().toString();
    const request = { name: name, pipelineId: null, previousTaskId: null } as ExecutableTask;
    if (this.pipeline.tasks.length == 0) {
      request.pipelineId = this.pipeline.pipeline.id;
    } else {
      const lastTask = this.pipeline.tasks[this.pipeline.tasks.length - 1];
      request.previousTaskId = lastTask.id;
    }

    this.tasks.create(request).subscribe(() => {
      this.ngOnInit();
    });
  }
}
