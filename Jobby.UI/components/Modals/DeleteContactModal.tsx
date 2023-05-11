"use client";

import { deleteAsync } from "@/lib/clientFetch";
import { useRouter } from "next/navigation";
import { useAuth } from "@clerk/nextjs";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui/alert-dialog";

interface Props {
  contactId: string | null;
}

export const DeleteContactModal = ({ contactId }: Props) => {
  const { getToken } = useAuth();

  const router = useRouter();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    await deleteAsync(`/contact/delete/${contactId}`, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    router.back();
  };

  return (
    <AlertDialog defaultOpen>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Delete Contact</AlertDialogTitle>
          <AlertDialogDescription>
            Are you sure you want to delete this contact?
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel onClick={router.back}>Cancel</AlertDialogCancel>
          <AlertDialogAction onClick={handleSubmit}>Delete</AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
};