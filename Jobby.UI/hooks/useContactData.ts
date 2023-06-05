import { clientApi } from "@/lib/clients/clientApi";
import { Contact } from "@/types";
import { useAuth } from "@clerk/nextjs";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AxiosResponse } from "axios";

export const useCreateContact = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function createContact(values: any) {
    return await clientApi.post<any, AxiosResponse<Contact>>(
      "/contact/create",
      values,
      {
        headers: {
          Authorization: `Bearer ${await getToken()}`,
        },
      }
    );
  }

  return useMutation(createContact, {
    onSuccess: async  ({ data: createdContact}) => {
      const queryKeys = [];

      // Global Contacts
      queryKeys.push(["/contacts"]);

      // Board Contacts
      if (createdContact.board?.id) {
        queryKeys.push(["/contacts", `/board/${createdContact.board.id}/contacts`]);
      }

      // Job Contacts
      if (createdContact.jobs.length > 0) {
        createdContact.jobs.forEach((job) => {
          queryKeys.push(["/contacts", `/job/${job.id}/contacts`]);
        });
      }

      console.log("queryKeys", queryKeys);

      // Only invalidate the queries that are affected by the new contact.
      await Promise.all(queryKeys.map((key) => queryClient.invalidateQueries({
        queryKey: key,
        refetchType: "all"
      })));

      // await queryClient.invalidateQueries(["/contacts"]);
    },
  });
};

export const useUpdateContact = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function updateContact(values: any) {
    return await clientApi.put<any, AxiosResponse<Contact>>("/contact/update", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
  }

  return useMutation(updateContact, {
    onSuccess: async  ({ data: updatedContact}) => {
      const previousQueries = queryClient.getQueriesData<Contact[]>(["contacts"]);

      previousQueries.map((query) => {
        queryClient.setQueryData(query[0], (prevContacts: Contact[] | undefined) =>
          prevContacts?.map((contact) => {
            if (contact?.id === updatedContact.id) {
              return updatedContact;
            }
            
            return contact;
          })
        );
      })
    },
  });
};

export const useDeleteContact = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function deleteContact(contactId: string) {
    const response = await clientApi.delete(`/contact/delete/${contactId}`, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    return [response, contactId];
  }

  return useMutation(deleteContact, {
    onSuccess: async (_, contactId) => {
      await queryClient.invalidateQueries({
        queryKey: ["/contacts"],
        refetchType: "all"
      });
    },
  });
};

export const useContactsQuery = (initialContacts: Contact[], url: string, queryKeyVariable?: string) => {
  const { getToken } = useAuth();

  const getContacts = async () => {
    const response = await clientApi.get<Contact[]>(url, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    return response.data;
  };

  const queryKey = queryKeyVariable ? ["/contacts", queryKeyVariable] : ["/contacts"];

   return useQuery<Contact[]>({
    queryKey,
    queryFn: getContacts,
    initialData: initialContacts,
  });
};
