import { Activity } from "@/types";
import { AxiosInstance, AxiosResponse } from "axios";
import { z } from "zod";

export const activityTypes = z.enum([
    "Apply",
    "Phone Screen",
    "Phone Interview",
    "On Site Interview",
    "Offer Received",
    "Accept Offer",
    "Prep Cover Letter",
    "Prep Resume",
    "Reach Out",
    "Get Reference",
    "Follow Up",
    "Prep For Interview",
    "Decline Offer",
    "Rejected",
    "Send Thank You",
    "Email",
    "Meeting",
    "Phone Call",
    "Send Availability",
    "Assignment",
    "Networking Event",
    "Application Withdrawn",
    "Other",
  ]);

  export const formSchema = z.object({
    title: z.string().nonempty(),
    type: activityTypes.transform((val) => Object.keys(activityTypes.Values).indexOf(val)),
    startDate: z.date().optional(),
    endDate: z.date().optional(),
    jobId: z.string().optional(),
    note: z.string().optional(),
    completed: z.boolean(),
    boardId: z.string(),
  });

export type CreateActivityRequest = z.infer<typeof formSchema>;

export const createActivity = async (data: CreateActivityRequest, client: AxiosInstance) => {
  return await client.post<CreateActivityRequest, AxiosResponse<Activity>>("/activity/create", data);
}