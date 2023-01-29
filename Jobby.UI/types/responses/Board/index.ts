export type BoardDictionaryResponse = {
  id: string;
  name: string;
  jobLists: JobListDictionaryResponse[];
}

type JobListDictionaryResponse = {
  id: string;
  name: string;
}