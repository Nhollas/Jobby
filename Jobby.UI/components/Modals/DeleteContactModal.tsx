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
import { useEffect, useState } from "react";

interface Props {
  contactId: string | null;
}

export const DeleteContactModal = ({ contactId }: Props) => {
  const { getToken } = useAuth();

  const [open, setOpen] = useState(false);

  useEffect(() => {
    setOpen(true);
  }, []);

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
    <AlertDialog open={open} onOpenChange={router.back}>
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
