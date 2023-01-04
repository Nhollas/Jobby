import { ActionButton, ModalContainer } from "../../Common";
import { client } from "../../../client";
import { Board } from "../../../types";

interface Props {
  setCurrentBoardList: (boards : any) => void;
  setShowDeleteModal: ({ visible } : { visible: boolean}) => void;
  visible: boolean;
  boardId: string;
}

export const DeleteBoardModal = (props : Props) => {
  const {
    setCurrentBoardList,
    setShowDeleteModal,
    visible,
    boardId,
  } = props;

  const handleSubmit = async (event) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    await client.delete(`/board/delete/${boardId}`);

    setCurrentBoardList((prev : Board[]) => prev.filter((board) => board.id !== boardId));

    setShowDeleteModal({
      visible: false,
    });
  };

  if (visible) {
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
              onClick={() =>
                setShowDeleteModal({
                  visible: false,
                })
              }
            />
            <ActionButton
              variant="danger"
              text="Delete"
              onClick={(e : any) => handleSubmit(e)}
              extended
            />
          </p>
        </div>
      </ModalContainer>
    );
  }
};
