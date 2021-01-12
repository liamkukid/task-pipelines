export interface ExecutableTask {
    id: number;
    name: string;
    duration: number;
    previousTaskId: number;
    pipelineId: number;
    createdAt: Date;
    updatedAt: Date;
}
