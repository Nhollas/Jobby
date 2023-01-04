import { ActionButton, ModalContainer } from "../../Common";
import { client } from "../../../client";
import { Board } from "../../../types";

interface Props {
  setCurrentBoardList: (boards : any) => void;
  setShowCreateModal: ({ visible } : { visible: boolean}) => void;
  visible: boolean;
}

export const CreateBoardModal = (props : Props) => {
  const { setCurrentBoardList, setShowCreateModal, visible } = props;

  const handleSubmit = async (event) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    var board = {
      name: event.target.name.value,
    };

    const createdBoard = await client.post<any, Board>("/board/create", board);

    setCurrentBoardList((prev: Board[]) => ([...prev, createdBoard]));

    setShowCreateModal({
      visible: false,
    });
  };

  if (visible) {
    return (
      <ModalContainer>
        <form
          onSubmit={handleSubmit}
          className="flex flex-col gap-y-8"
          method="post"
        >
          <h1 className="text-xl font-medium">Create Board</h1>
          <p className="flex flex-col gap-y-2">
            <label htmlFor="name"></label>
            <input id="name" name="name"></input>
          </p>
          <p className="flex flex-row justify-center gap-4">
            <ActionButton
              variant="secondary"
              text="Cancel"
              onClick={() =>
                setShowCreateModal({
                  visible: false,
                })
              }
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
  }
};
