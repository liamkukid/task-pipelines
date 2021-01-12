import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ExecutableTask } from "../models/executable-task";
import { ApiService } from "./api.service";

@Injectable({
    providedIn: 'root'
})
export class ExecutableTasksService {
    private readonly url = '/api/executable-tasks'
    constructor(private readonly api: ApiService) {}

    all(): Observable<Array<ExecutableTask>> {
        return this.api.get<Array<ExecutableTask>>(this.url);
    }

    get(id: number): Observable<ExecutableTask> {
        return this.api.get<ExecutableTask>(this.url + '/' + id);
    }

    ofPipeline(pipelineId: number): Observable<Array<ExecutableTask>> {
        return this.api.get<Array<ExecutableTask>>(this.url + '/pipeline/' + pipelineId);
    }

    create(task: ExecutableTask): Observable<void> {
        return this.api.post<void>(this.url, task);
    }

    delete(id: number): Observable<void> {
        return this.api.delete<void>(this.url + '/' + id);
    }
}