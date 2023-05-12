"use client";

import { deleteAsync } from "@/lib/clientFetch";
import { Board } from "types";
import { useContext, useEffect, useState } from "react";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
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
  boardId: string | null;
}

export const DeleteBoardModal = ({ boardId }: Props) => {
  const { setBoards } = useContext(BoardsAndJobsContext);
  const { getToken } = useAuth();

  const [open, setOpen] = useState(false);

  useEffect(() => {
    setOpen(true);
  }, []);

  const router = useRouter();

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    await deleteAsync(`/board/delete/${boardId}`, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    setBoards((prev: Board[]) => prev.filter((board) => board.id !== boardId));

    router.back();
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
          <AlertDialogCancel onClick={router.back}>Cancel</AlertDialogCancel>
          <AlertDialogAction onClick={handleSubmit}>Delete</AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
};
