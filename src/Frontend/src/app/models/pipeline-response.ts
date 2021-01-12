import { ExecutableTask } from './executable-task';
import { Pipeline } from './pipeline';

export interface PipelineResponse {
    pipeline: Pipeline;
    tasks: Array<ExecutableTask>;
}
