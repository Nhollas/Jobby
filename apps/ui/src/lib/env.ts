import { z } from "zod";

export const env = () => z.object({
    JOBBY_API_BASE_URL: z.string(),
}).parse(process.env);
