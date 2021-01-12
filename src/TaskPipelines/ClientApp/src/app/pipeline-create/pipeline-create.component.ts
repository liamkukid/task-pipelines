import { Component, OnInit } from '@angular/core';
import { Pipeline } from '../models/pipeline';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-pipeline-create',
  templateUrl: './pipeline-create.component.html'
})
export class PipelineCreateComponent implements OnInit {
  constructor(private readonly api: ApiService) {}

  ngOnInit(): void {
    this.api.get<Array<Pipeline>>('/api/pipelines').subscribe(x => {
      console.log(x);
    });
  }
}
