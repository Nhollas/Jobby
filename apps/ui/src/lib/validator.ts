import { z } from "zod";

export function validator<S extends z.ZodTypeAny, T>(
  payload: any,
  schema: S
): z.infer<S> {
  if (typeof payload !== "object") {
    throw new Error("Payload must be an object");
  }

  const validatedPayload = schema.safeParse(payload);

  if (!validatedPayload.success) {
    throw new Error(validatedPayload.error.message);
  }

  return validatedPayload.data;
}
