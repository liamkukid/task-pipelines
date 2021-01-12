import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Pipeline } from '../models/pipeline';
import { PipelineResponse } from '../models/pipeline-response';
import { ApiService } from './api.service';

@Injectable({
    providedIn: 'root'
})
export class PipelinesService {
    private readonly url = '/api/pipelines'
    constructor(private readonly api: ApiService) {}

    all(): Observable<Array<PipelineResponse>> {
        return this.api.get<Array<PipelineResponse>>(this.url);
    }

    get(id: number): Observable<Pipeline> {
        return this.api.get<Pipeline>(this.url + '/' + id);
    }

    create(pipeline: Pipeline): Observable<void> {
        return this.api.post<void>(this.url, pipeline);
    }

    delete(id: number): Observable<void> {
        return this.api.delete<void>(this.url + '/' + id);
    }
}