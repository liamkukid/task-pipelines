export interface ExecutableTask {
    id: string;
    name: string;
    duration: number;
    previousTaskId: string;
    pipelineId: string;
    createdAt: Date;
    updatedAt: Date;
}
