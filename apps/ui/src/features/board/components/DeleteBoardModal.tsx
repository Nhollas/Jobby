"use client";

import { useEffect, useState } from "react";
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
} from "@/components/ui";
import { useDeleteBoard } from "@/features/board";

interface Props {
  boardRef: string;
}

export const DeleteBoardModal = ({ boardRef }: Props) => {
  const [open, setOpen] = useState(false);

  useEffect(() => {
    setOpen(true);
  }, []);

  const router = useRouter();

  const { mutateAsync } = useDeleteBoard();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    await mutateAsync(boardRef);

    setOpen(false);

    router.push("/track/boards");
  };

  return (
    <AlertDialog open={open} onOpenChange={router.back}>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Delete Board</AlertDialogTitle>
          <AlertDialogDescription>
            Are you sure you want to delete this board?
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel
            onClick={() => {
              setOpen(false);
              router.push("/track/boards");
            }}
          >
            Cancel
          </AlertDialogCancel>
          <AlertDialogAction onClick={(e) => handleSubmit(e)}>
            Confirm
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
};
