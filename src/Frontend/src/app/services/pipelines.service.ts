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

    get(id: string): Observable<PipelineResponse> {
        return this.api.get<PipelineResponse>(this.url + '/' + id);
    }

    create(): Observable<{id: string}> {
        return this.api.post<{id: string}>(this.url);
    }

    delete(id: string): Observable<void> {
        return this.api.delete<void>(this.url + '/' + id);
    }

    start(id: string): Observable<void> {
        return this.api.get<void>(`${this.url}/${id}/start`);
    }
}