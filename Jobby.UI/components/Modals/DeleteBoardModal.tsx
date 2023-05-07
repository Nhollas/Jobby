"use client";

import { ActionButton, ModalContainer } from "../Common";
import { deleteAsync } from "app/serverClient";
import { Board } from "types";
import { useContext } from "react";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import { useRouter } from "next/navigation";
import { useAuth } from "@clerk/nextjs";

interface Props {
  boardId: string | null;
}

export const DeleteBoardModal = ({ boardId }: Props) => {
  const { setBoards } = useContext(BoardsAndJobsContext);
  const { getToken } = useAuth();

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
    <ModalContainer>
      <div className="flex flex-col gap-6">
        <h1 className="overflow-hidden text-ellipsis whitespace-nowrap text-xl font-medium">
          Delete Board
        </h1>
        <p>Are you sure you want to delete this board?</p>
        <p className="flex flex-row justify-center gap-4">
          <ActionButton
            variant="secondary"
            text="Cancel"
            onClick={() => router.back()}
          />
          <ActionButton
            variant="danger"
            text="Delete"
            onClick={handleSubmit}
            type="button"
            extended
          />
        </p>
      </div>
    </ModalContainer>
  );
};
