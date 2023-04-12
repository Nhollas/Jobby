"use client";

import { ActionButton } from "../Common";
import { client } from "clients";
import { Board } from "types";
import { Dispatch, SetStateAction, useContext, useState } from "react";
import { ModalContext } from "contexts/ModalContext";
import Input from "../Common/Input";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";

interface Props {
  setCurrentBoardList: Dispatch<SetStateAction<Board[]>>;
}

export const CreateBoardModal = ({ setCurrentBoardList }: Props) => {
  const { closeModal } = useContext(ModalContext);
  const { setBoards } = useContext(BoardsAndJobsContext);

  const [name, setName] = useState("");

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    var board = {
      name,
    };

    const createdBoard = await client.post<any, Board>("/board/create", board);

    setCurrentBoardList((prev: Board[]) => [...prev, createdBoard]);
    setBoards((prev: Board[]) => [...prev, createdBoard]);
    
    closeModal();
  };

  return (
    <form
      onSubmit={handleSubmit}
      className='flex flex-col gap-y-8'
      method='post'
    >
      <h1 className='text-xl font-medium'>Create Board</h1>
      <Input
        name='name'
        label='Name'
        type='text'
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <p className='flex flex-row justify-center gap-4'>
        <ActionButton
          variant='secondary'
          text='Cancel'
          onClick={() => closeModal()}
        />
        <ActionButton variant='primary' text='Create' type='submit' extended />
      </p>
    </form>
  );
};
