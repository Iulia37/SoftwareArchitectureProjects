export type Task = {
    id: number;
    projectId: number;
    title: string;
    description: string,
    deadline: Date,
    isCompleted: boolean;
    createdDate: Date;
}