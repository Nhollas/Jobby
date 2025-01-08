import { z } from "zod"

const envSchema = z.object({
  NEXT_PUBLIC_OTEL_COLLECTOR_URL: z.string(),
})

export const env = envSchema.parse(process.env)

export type Env = z.infer<typeof envSchema>
