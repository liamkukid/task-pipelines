export interface Pipeline {
    id: number;
    createdAt: Date;
    updatedAt: Date;

    startedAt: Date | null;
    finishedAt: Date | null;
}
