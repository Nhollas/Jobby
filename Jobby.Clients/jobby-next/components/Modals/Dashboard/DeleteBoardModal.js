import { deleteBoard } from "../../../services/board/boardService"

export const DeleteBoardModal = (props) => {

  const handleSubmit = async (event) => {
    event.preventDefault()

    //TODO: Validation with YUP package.

    var boardId = event.target.boardId.value
    
    await deleteBoard(boardId)

    // Remove deleted board from the BoardList rather than having to do another fetch.
    const selectedBoard = props.boardList.findIndex((obj) => obj.id == boardId)
    props.boardList.splice(selectedBoard, 1)

    props.boardListState(props.boardList)

    closeModal()
  }

  const closeModal = () => {
    props.parentState({
      visible: false
    })
  }

  if (props.visible == true) {
    return (
      <div className="absolute inset-0 w-full h-full flex justify-center bg-white/90">
        <div className="w-full max-w-md border-1 h-max border-gray-300 p-6 bg-gray-50">
          <form className="flex flex-col gap-6" method="post" onSubmit={handleSubmit}>
            <h1 className="text-xl font-medium text-ellipsis overflow-hidden whitespace-nowrap">Delete Board</h1>
            <p>Are you sure you want to delete this board?</p>
            <input id="boardId" name="boardId" type="hidden" defaultValue={props.board.id}></input>
            <p className="flex flex-row gap-4 justify-center">
              <button 
                onClick={closeModal}
                type="button"  
                className="font-medium border-1 border-gray-300 bg-white font-raleway py-2 px-4">
                Cancel
              </button>
              <button 
                type="submit" 
                className="text-white font-raleway font-medium text-base bg-main-blue hover:bg-gray-50 hover:text-black hover:border-main-blue border-1 w-full py-2">
                Delete
              </button>
            </p>
          </form>
        </div>
      </div>
    )
  } else {
    return null
  }

}