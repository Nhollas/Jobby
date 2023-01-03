import { useReducer } from "react";
import { updateBoard } from "../../../services/boardService";
import ActionButton from "../../Common/ActionButton";
import ModalContainer from "../../Common/ModalContainer";
import Input from "../../Form/Input";

export const EditBoardModal = (props) => {
  const { setCurrentBoardList, setShowEditModal, visible, accessToken, board } =
    props;

  const reducer = (state, action) => {
    const { name, value } = action;
    switch (action.type) {
      case "HANDLE_INPUT_CHANGE":
        return {
          ...state,
          [name]: value,
        };
      default:
        return state;
    }
  };

  const [state, dispatch] = useReducer(reducer, { board });

  const handleChange = (event) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: event.target.name,
      value: event.target.value,
    });
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    var name = event.target.name.value;

    await updateBoard(board.id, name, accessToken);

    var updatedBoard = {
      id: board.id,
      createdDate: board.createdDate,
      name: name,
    };

    setCurrentBoardList((prev) =>
      prev.map((board) => {
        if (board.id === updatedBoard.id) {
          return updatedBoard;
        }
        return board;
      })
    );

    setShowEditModal({
      visible: false,
      board: null,
    });
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
            type='hidden'
            className='hidden'
            value={board.id}
            name='boardId'
          />
          <Input
            type='text'
            name='name'
            value={state.name}
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
