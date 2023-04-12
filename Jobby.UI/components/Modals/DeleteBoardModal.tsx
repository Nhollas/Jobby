"use client";

import { ActionButton } from "../Common";
import { client } from "clients";
import { Board } from "types";
import { Dispatch, SetStateAction, useContext } from "react";
import { ModalContext } from "contexts/ModalContext";
import BoardsAndJobsContext from "contexts/BoardsAndJobsContext";

interface Props {
  setCurrentBoardList: Dispatch<SetStateAction<Board[]>>;
  boardId: string | null;
}

export const DeleteBoardModal = ({ setCurrentBoardList, boardId }: Props) => {
  const { closeModal } = useContext(ModalContext);
  const { setBoards } = useContext(BoardsAndJobsContext);

  const handleSubmit = async (event: any) => {
    event.preventDefault();

    await client.delete(`/board/delete/${boardId}`);

    setCurrentBoardList((prev: Board[]) =>
      prev.filter((board) => board.id !== boardId)
    );

    setBoards((prev: Board[]) =>
      prev.filter((board) => board.id !== boardId)
    );
    
    closeModal();
  };

  return (
    <div className='flex flex-col gap-6'>
      <h1 className='overflow-hidden text-ellipsis whitespace-nowrap text-xl font-medium'>
        Delete Board
      </h1>
      <p>Are you sure you want to delete this board?</p>
      <p className='flex flex-row justify-center gap-4'>
        <ActionButton
          variant='secondary'
          text='Cancel'
          onClick={() => closeModal()}
        />
        <ActionButton
          variant='danger'
          text='Delete'
          onClick={handleSubmit}
          extended
        />
      </p>
    </div>
  );
};
