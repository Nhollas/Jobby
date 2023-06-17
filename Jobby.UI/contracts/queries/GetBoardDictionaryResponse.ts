export type GetBoardDictionaryResponse = {
    id: string;
    name: string;
    jobLists: JobList[]
}

type JobList = {
    id: string;
    name: string;
}