import { nextApiClient } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Contact } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const CreateContactSchema = z.object({
  boardReference: z.string(),
  jobReferences: z.array(z.string()),
  firstName: z.string(),
  lastName: z.string(),
  jobTitle: z.string(),
  location: z.string(),
  socials: z.object({
    twitterUrl: z.string(),
    facebookUrl: z.string(),
    linkedInUrl: z.string(),
    githubUrl: z.string(),
  }),
  emails: z.array(
    z.object({
      name: z.string(),
      type: z.number(),
    })
  ),
  phones: z.array(
    z.object({
      number: z.string(),
      type: z.number(),
    })
  ),
  companies: z.array(z.string()),
});

export type CreateContactDTO = z.infer<typeof CreateContactSchema>;

export async function createContact(payload: CreateContactDTO) {
  try {
    const response = await nextApiClient.post<
      any,
      AxiosResponse<Contact>,
      CreateContactDTO
    >("/contact", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useCreateContact = () => {
  return useMutation({
    onMutate: async () => {
      const previousContacts = queryClient.getQueryData<Contact[]>([
        "contacts",
      ]);

      return { previousContacts };
    },
    onError: (_, __, context: any) => {
      if (context?.previousContacts) {
        queryClient.setQueryData(["contacts"], context.previousContacts);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["contacts"]),
    mutationFn: createContact,
  });
};
