export type JobLists = {
  id: string;
  name: string;
  count: number;
  createdDate: string;
  jobs: Job[];
};

export type Job = {
  id: string;
  createdDate: string;
  lastUpdated: string;
  company: string;
  title: string;
};
