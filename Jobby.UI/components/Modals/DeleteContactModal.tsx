"use client";

import { useRouter } from "next/navigation";
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
import { useDeleteContact } from "@/hooks/useContactData";

interface Props {
  contactId: string;
}

export const DeleteContactModal = ({ contactId }: Props) => {
  const [open, setOpen] = useState(false);
  const { mutateAsync } = useDeleteContact();

  useEffect(() => {
    setOpen(true);
  }, []);

  const router = useRouter();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    await mutateAsync(contactId);

    router.back();
  };

  return (
    <AlertDialog open={open}>
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
