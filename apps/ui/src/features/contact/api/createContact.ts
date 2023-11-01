import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Activity, Contact } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type CreateContactDTO = {
  boardReference: string;
  jobReferences: string[];
  firstName: string;
  lastName: string;
  jobTitle: string;
  location: string;
  social: {
    twitterUrl: string;
    facebookUrl: string;
    linkedinUrl: string;
    githubUrl: string;
  };
  emails: {
    name: string;
    type: number;
  }[];
  phones: {
    number: string;
    type: number;
  }[];
  companies: string[];
};

export async function createContact(payload: CreateContactDTO) {
  try {
    const response = await client.post<
      any,
      AxiosResponse<Contact>,
      CreateContactDTO
    >("/contact", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useCreateActivity = () => {
  return useMutation({
    onMutate: async () => {
      const previousContacts = queryClient.getQueryData<Activity[]>([
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
