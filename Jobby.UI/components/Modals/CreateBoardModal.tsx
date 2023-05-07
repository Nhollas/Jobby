"use client";

import { ActionButton, ModalContainer } from "../Common";
import { Board } from "types";
import { useContext, useState } from "react";
import Input from "../Common/Input";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";
import { useRouter } from "next/navigation";
import { postAsync } from "app/serverClient";
import { useAuth } from "@clerk/nextjs";

export const CreateBoardModal = () => {
  const { setBoards } = useContext(BoardsAndJobsContext);
  const { getToken } = useAuth();

  const router = useRouter();
  const [name, setName] = useState("");

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    var board = {
      name,
    };

    const createdBoard = await postAsync<any, Board>("/board/create", board, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    setBoards((prev: Board[]) => [...prev, createdBoard]);

    router.back();
  };

  return (
    <ModalContainer>
      <form
        onSubmit={handleSubmit}
        className="flex flex-col gap-y-8"
        method="post"
      >
        <h1 className="text-xl font-medium">Create Board</h1>
        <Input
          name="name"
          label="Name"
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <p className="flex flex-row justify-center gap-4">
          <ActionButton
            variant="secondary"
            text="Cancel"
            onClick={() => router.back()}
            type="button"
          />
          <ActionButton
            variant="primary"
            text="Create"
            type="submit"
            extended
          />
        </p>
      </form>
    </ModalContainer>
  );
};
