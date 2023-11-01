import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Activity, Contact } from "@/types";
import { useMutation } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export type UpdateContactDTO = {
  contactReference: string;
  firstName: string;
  lastName: string;
  jobTitle: string;
  location: string;
  socials: {
    twitterUrl: string;
    facebookUrl: string;
    linkedinUrl: string;
    githubUrl: string;
  };
  jobReferences: string[];
  boardReference: string;
  emails: {
    reference: string;
    name: string;
    type: number;
  }[];
  phones: {
    reference: string;
    number: string;
    type: number;
  }[];
  companies: {
    reference: string;
    name: string;
  }[];
};

export async function updateContact(payload: UpdateContactDTO) {
  try {
    const response = await client.put<
      any,
      AxiosResponse<Contact>,
      UpdateContactDTO
    >("/contact", payload);
    return response.data;
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useUpdateActivity = () => {
  return useMutation({
    onMutate: async () => {
      const previousContacts = queryClient.getQueryData<Activity[]>([
        "contacts",
      ]);

      return { previousContacts };
    },
    onError: (error, __, context: any) => {
      console.error(error);

      if (context?.previousContacts) {
        queryClient.setQueryData(["contacts"], context.previousContacts);
      }
    },
    onSuccess: () => queryClient.invalidateQueries(["contacts"]),
    mutationFn: updateContact,
  });
};
