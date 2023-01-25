export type CreateJobListRequest = {
  boardId: string;
  name: string;
  index: number;
  initJobId: string | null;
}