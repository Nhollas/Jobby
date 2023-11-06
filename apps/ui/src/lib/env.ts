import { z } from "zod";

export const env = z.object({}).parse(process.env);
