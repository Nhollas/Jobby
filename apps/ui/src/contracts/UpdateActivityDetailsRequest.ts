import { z } from "zod";

export type UpdateActivityDetailsRequest = z.infer<typeof activitySchema>;

const activitySchema = z.object({
    id: z.string(),
    title: z.string(),
    type: z.number(),
    startDate: z.date(),
    endDate: z.date(),
    note: z.string(),
    completed: z.boolean(),
  });