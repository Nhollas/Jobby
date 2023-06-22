import { createContact, CreateContactRequest } from "@/contracts/CreateContact";
import { updateContact, UpdateContactDetailsRequest } from "@/contracts/UpdateContactDetailsRequest";
import { useClientApi } from "@/lib/clients";
import { Contact } from "@/types";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

export const useCreateContact = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();

  return useMutation({
    mutationFn: (values: CreateContactRequest) => createContact(values, clientApi),
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

      console.log("queryKeys",queryKeys)

      // Only invalidate the queries that are affected by the new contact.
      await Promise.all(queryKeys.map((key) => queryClient.invalidateQueries({
        queryKey: key,
        refetchType: "all"
      })));
    },
  });
};

export const useUpdateContact = () => {
  const queryClient = useQueryClient();
  const clientApi = useClientApi();

  return useMutation({
    mutationFn: (values: UpdateContactDetailsRequest) => updateContact(values, clientApi),
    onSuccess: async  ({ data: updatedContact }) => {
      console.log("updatedContact",updatedContact)

      const previousQueries = queryClient.getQueriesData({ queryKey: ["/contacts"]});
      console.log("prevContactQueries",previousQueries)

      previousQueries.map((query) => {
        queryClient.setQueryData(query[0], (prevContacts: Contact[] | undefined) =>
          prevContacts?.map((contact) => {
            if (contact.id === updatedContact.id) {
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
  const clientApi = useClientApi();

  async function deleteContact(contactId: string) {
    const response = await clientApi.delete(`/contact/delete/${contactId}`);

    return [response, contactId];
  }

  return useMutation(deleteContact, {
    onSuccess: async (_, contactId) => {
      console.log("sus")

      await queryClient.invalidateQueries({
        queryKey: ["/contacts"],
        refetchType: "all"
      });
    },
  });
};

export const useContactsQuery = (initialContacts: Contact[], url: string, queryKeyVariable?: string) => {
  const clientApi = useClientApi();

  const getContacts = async () => {
    const response = await clientApi.get<Contact[]>(url);

    return response.data;
  };

  const queryKey = queryKeyVariable ? ["/contacts", queryKeyVariable] : ["/contacts"];

   return useQuery<Contact[]>({
    queryKey,
    queryFn: getContacts,
    initialData: initialContacts,
    staleTime: Infinity,
    cacheTime: Infinity,
  });
};
