import { Job } from "@/types";
import { AxiosInstance, AxiosResponse } from "axios";
import { z } from "zod";

export type CreateJobRequest = z.infer<typeof formSchema>;

export const formSchema = z.object({
  title: z.string().nonempty(),
  company: z.string().nonempty(),
  jobListId: z.string(),
  boardId: z.string(),
});

export const createJob = async (data: CreateJobRequest, client: AxiosInstance) => {
  return await client.post<CreateJobRequest, AxiosResponse<Job>>("/job/create", data);
}