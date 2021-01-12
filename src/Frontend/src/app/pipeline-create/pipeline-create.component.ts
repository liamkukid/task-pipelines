import { Component, OnInit } from '@angular/core';
import { Pipeline } from '../models/pipeline';
import { PipelinesService } from '../services/pipelines.service';

@Component({
  selector: 'app-pipeline-create',
  templateUrl: './pipeline-create.component.html'
})
export class PipelineCreateComponent implements OnInit {
  constructor(private readonly api: PipelinesService) {}

  ngOnInit(): void {
    this.api.all().subscribe(x => {
      console.log(x);
    });
  }
}
