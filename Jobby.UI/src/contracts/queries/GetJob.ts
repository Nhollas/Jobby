import { AxiosInstance } from "axios";

export type GetJobResponse = {

}

export const getJob = async (jobId: string, client: AxiosInstance) => await client.get<GetJobResponse>(`/job/${jobId}`)