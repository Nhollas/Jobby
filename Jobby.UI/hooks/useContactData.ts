import { clientApi } from "@/lib/clients/clientApi";
import { Contact } from "@/types";
import { useAuth } from "@clerk/nextjs";
import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useCreateContact = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function createContact(values: any) {
    return await clientApi.post<any, Contact>("/contact/create", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
  }

  return useMutation(createContact, {
    onSuccess: (createdContact) => {
      queryClient.setQueryData(
        ["contacts"],
        (prevData: Contact[] | undefined) => {
          return prevData ? [...prevData, createdContact] : [createdContact];
        }
      );
    },
  });
};

export const useUpdateContact = () => {
  const queryClient = useQueryClient();
  const { getToken } = useAuth();

  async function updateContact(values: any) {
    return await clientApi.post<any, Contact>("/contact/update", values, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });
  }

  return useMutation(updateContact, {
    onSuccess: (updatedContact) => {
      queryClient.setQueryData(
        ["contacts"],
        (prevData: Contact[] | undefined) => {
          if (prevData) {
            return prevData.map((contact) => {
              if (contact.id === updatedContact.id) {
                return updatedContact;
              }
              return contact;
            });
          }
          return [updatedContact];
        }
      );
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
    onSuccess: ([_, contactId]) => {
      queryClient.setQueryData(
        ["contacts"],
        (prevData: Contact[] | undefined) =>
          prevData?.filter((contact) => contact.id !== contactId)
      );
    },
  });
};

export function useMutateContact() {
  return {
    useCreateContact,
    useUpdateContact,
    useDeleteContact,
  };
}
