import { deleteBoard } from "../../../services/boardService";
import ActionButton from "../../Common/ActionButton";
import ModalContainer from "../../Common/ModalContainer";

export const DeleteBoardModal = (props) => {
  const {
    setCurrentBoardList,
    setShowDeleteModal,
    visible,
    accessToken,
    boardId,
  } = props;
  const handleSubmit = async (event) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    await deleteBoard(boardId, accessToken);

    setCurrentBoardList((prev) => prev.filter((board) => board.id !== boardId));

    setShowDeleteModal({
      visible: false,
    });
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
                setShowDeleteModal({
                  visible: false,
                })
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
