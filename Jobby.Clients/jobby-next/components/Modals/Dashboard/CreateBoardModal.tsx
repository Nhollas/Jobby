import { ActionButton, ModalContainer } from "../../Common";
import { client } from "../../../client";
import { Board } from "../../../types";
import { Dispatch, SetStateAction } from "react";

interface Props {
  setCurrentBoardList: Dispatch<SetStateAction<Board[]>>;
  setShowCreateModal: (modalState: { visible: boolean }) => void;
  showCreateModal: { visible: boolean };
}

export const CreateBoardModal = ({
  setCurrentBoardList,
  setShowCreateModal,
  showCreateModal,
}: Props) => {
  const { visible } = showCreateModal;
  const handleSubmit = async (event) => {
    event.preventDefault();

    var board = {
      name: event.target.name.value,
    };

    const createdBoard = await client.post<any, Board>("/board/create", board);

    setCurrentBoardList((prev: Board[]) => [...prev, createdBoard]);
    setShowCreateModal({ visible: false });
  };

  if (visible) {
    return (
      <ModalContainer>
        <form
          onSubmit={handleSubmit}
          className='flex flex-col gap-y-8'
          method='post'
        >
          <h1 className='text-xl font-medium'>Create Board</h1>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='name'></label>
            <input id='name' name='name'></input>
          </p>
          <p className='flex flex-row justify-center gap-4'>
            <ActionButton
              variant='secondary'
              text='Cancel'
              onClick={() =>
                setShowCreateModal({
                  visible: false,
                })
              }
            />
            <ActionButton
              variant='primary'
              text='Create'
              type='submit'
              extended
            />
          </p>
        </form>
      </ModalContainer>
    );
  }
};
