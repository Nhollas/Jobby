import { Reducer, useReducer } from "react";
import { client } from "../../../client";
import { Board } from "../../../types";
import { ActionButton, ModalContainer } from "../../Common";
import Input from "../../Form/Input";
import { Dispatch, SetStateAction } from "react";
import reducer from "../../../reducers/EditBoardModalReducer";

interface Props {
  setCurrentBoardList: Dispatch<SetStateAction<Board[]>>;
  setShowEditModal: ({
    visible,
    board,
  }: {
    visible: boolean;
    board: Board;
  }) => void;
  showEditModal: { visible: boolean; board: Board | null };
}

export const EditBoardModal = ({
  setCurrentBoardList,
  setShowEditModal,
  showEditModal,
}: Props) => {
  const { visible, board } = showEditModal;

  const [state, dispatch] = useReducer<Reducer<{ board: Board }, any>>(
    reducer,
    { board }
  );

  const handleChange = (event) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: event.target.name,
      value: event.target.value,
    });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const boardModal = {
      id: board.id,
      name: event.target.name.value as string,
    };

    await client.put("/board/update", {
      id: board.id,
      name: event.target.name.value,
    });

    setCurrentBoardList((prev: Board[]) =>
      prev.map((prevBoard) => {
        if (prevBoard.id === board.id) {
          return {
            ...prevBoard,
            name: boardModal.name,
          };
        }
        return prevBoard;
      })
    );

    setShowEditModal({ visible: false, board: null });
  };

  if (visible) {
    return (
      <ModalContainer>
        <form
          className='flex flex-col gap-6'
          method='post'
          onSubmit={handleSubmit}
        >
          <h1 className='overflow-hidden text-ellipsis whitespace-nowrap text-xl font-medium'>
            Edit Board - {board.name}
          </h1>
          <Input
            type='text'
            name='name'
            value={state.board.name}
            label='Name'
            onChange={(e) => handleChange(e)}
          />
          <div className='flex flex-row justify-center gap-4'>
            <ActionButton
              variant='secondary'
              text='Cancel'
              onClick={() =>
                setShowEditModal({
                  visible: false,
                  board: null,
                })
              }
            />
            <ActionButton
              variant='primary'
              text='Update'
              type='submit'
              extended
            />
          </div>
        </form>
      </ModalContainer>
    );
  }
};
