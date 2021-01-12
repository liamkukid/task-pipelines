export interface Pipeline {
    id: string;
    createdAt: Date;
    updatedAt: Date;

    startedAt: Date | null;
    finishedAt: Date | null;
}
