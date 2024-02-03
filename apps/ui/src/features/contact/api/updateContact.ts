import { nextApiClient } from "@/lib/clients";
import { queryClient } from "@/lib/react-query";
import { Activity, Contact } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";
import { z } from "zod";

export const UpdateContactSchema = z.object({
  contactReference: z.string(),
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
  jobReferences: z.array(z.string()),
  boardReference: z.string(),
  emails: z.array(
    z.object({
      reference: z.string(),
      name: z.string(),
      type: z.number(),
    })
  ),
  phones: z.array(
    z.object({
      reference: z.string(),
      number: z.string(),
      type: z.number(),
    })
  ),
  companies: z.array(
    z.object({
      reference: z.string(),
      name: z.string(),
    })
  ),
});

export type UpdateContactDTO = z.infer<typeof UpdateContactSchema>;

export async function updateContact(payload: UpdateContactDTO) {
  try {
    const response = await nextApiClient.put<
      any,
      AxiosResponse<Contact>,
      UpdateContactDTO
    >("/contact", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useUpdateContact = () => {
  return useMutation({
    onMutate: async () => {
      const previousContacts = queryClient.getQueryData<Activity[]>([
        "contacts",
      ]);

      return { previousContacts };
    },
    onError: (error, __, context: any) => {
      if (context?.previousContacts) {
        queryClient.setQueryData(["contacts"], context.previousContacts);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["contacts"]),
    mutationFn: updateContact,
  });
};
