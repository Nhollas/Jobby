import { client } from "@/lib/client";
import { queryClient } from "@/lib/react-query";
import { Contact } from "@/types";
import { useMutation } from "@tanstack/react-query";

export async function deleteContact(contactReference: string) {
  try {
    await client.delete(`/contact/${contactReference}`);
  } catch (error) {
    return Promise.reject(error);
  }
}

export const useDeleteContact = () => {
  return useMutation({
    onMutate: async (contactReference) => {
      await queryClient.cancelQueries(["contacts"]);

      const previousContacts = queryClient.getQueryData<Contact[]>([
        "contacts",
      ]);

      queryClient.setQueryData(
        ["contacts"],
        previousContacts?.filter(
          (board) => board.reference !== contactReference
        )
      );

      return { previousContacts };
    },
    onSuccess: () => {
      console.log("Going to invalidate contacts queries");
      queryClient.invalidateQueries(["contacts"]);
    },
    onError: (_, __, context: any) => {
      if (context?.previousContacts) {
        queryClient.setQueryData(["contacts"], context.previousContacts);
      }
    },
    mutationFn: deleteContact,
  });
};
