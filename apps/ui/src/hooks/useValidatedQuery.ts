import { useMutation, useQuery } from "@tanstack/react-query";
import { z } from "zod";

// Define a generic type for the request payload
type RequestPayload<T> = {
  data: T;
  schema: z.ZodType<T>;
};

// Define a generic type for the response payload
type ResponsePayload<T> = {
  data: T;
  schema: z.ZodType<T>;
};

// Define a custom hook for handling validation with Zod
export const useValidatedQuery = <T, R>(
  queryKey: string,
  queryFn: (payload: T) => Promise<R>,
  requestSchema: z.ZodType<T>,
  responseSchema: z.ZodType<R>
) => {
  // Define a wrapper function for the query function that handles validation
  const validatedQueryFn = async (payload: T) => {
    const validatedPayload = requestSchema.parse(payload);
    const response = await queryFn(validatedPayload);
    const validatedResponse = responseSchema.parse(response);
    return validatedResponse;
  };

  // Use the useQuery hook from React Query with the wrapper function
  const queryResult = useQuery<R>([queryKey], validatedQueryFn);

  return queryResult;
};

// Define a custom hook for handling validation with Zod
export const useValidatedMutation = <T, R>(
  mutationFn: (payload: T) => Promise<R>,
  requestSchema: z.ZodType<T>,
  responseSchema: z.ZodType<R>
) => {
  // Define a wrapper function for the mutation function that handles validation
  const validatedMutationFn = async (payload: T) => {
    try {
      const validatedPayload = requestSchema.parse(payload);
      const response = await mutationFn(validatedPayload);
      const validatedResponse = responseSchema.parse(response);
      return validatedResponse;
    } catch (error) {
      return undefined;
    }
  };

  // Use the useMutation hook from React Query with the wrapper function
  const mutationResult = useMutation<R>({
    mutationFn: validatedMutationFn,
  });

  return mutationResult;
};
