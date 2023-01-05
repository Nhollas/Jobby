import { ActionButton, ModalContainer } from "../../Common";
import { client } from "../../../client";
import { Board } from "../../../types";
import { Dispatch, SetStateAction } from "react";

interface Props {
  setCurrentBoardList: Dispatch<SetStateAction<Board[]>>;
  setShowDeleteModal: (modalState: {
    visible: boolean;
    boardId: string | null;
  }) => void;
  showDeleteModal: { visible: boolean; boardId: string | null };
}

export const DeleteBoardModal = ({
  setCurrentBoardList,
  setShowDeleteModal,
  showDeleteModal,
}: Props) => {
  const { visible, boardId } = showDeleteModal;

  const handleSubmit = async (event) => {
    event.preventDefault();

    await client.delete(`/board/delete/${boardId}`);

    setCurrentBoardList((prev: Board[]) =>
      prev.filter((board) => board.id !== boardId)
    );

    setShowDeleteModal({ visible: false, boardId: null });
  };

  if (visible) {
    return (
      <ModalContainer>
        <div className='flex flex-col gap-6'>
          <h1 className='overflow-hidden text-ellipsis whitespace-nowrap text-xl font-medium'>
            Delete Board
          </h1>
          <p>Are you sure you want to delete this board?</p>
          <p className='flex flex-row justify-center gap-4'>
            <ActionButton
              variant='secondary'
              text='Cancel'
              onClick={() =>
                setShowDeleteModal({ visible: false, boardId: null })
              }
            />
            <ActionButton
              variant='danger'
              text='Delete'
              onClick={(e) => handleSubmit(e)}
              extended
            />
          </p>
        </div>
      </ModalContainer>
    );
  }
};
