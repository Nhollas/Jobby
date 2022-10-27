import { updateBoard } from "../../../services/board/boardService"

export const EditBoardModal = (props) => {

  const handleSubmit = async (event) => {
    event.preventDefault()

    //TODO: Validation with YUP package.

    var name = event.target.name.value
    
    
    await updateBoard(props.board.id, name)

    // Update the board from the BoardList rather than having to do another fetch.
    const selectedBoard = props.boardList.findIndex((obj) => obj.id == props.board.id)

    var updatedBoard = {
      id:props.board.id,
      createdDate: props.board.createdDate,
      name: name
    }
    
    // Replace existing board with its updated info in the array order it was originally in.
    props.boardList.splice(selectedBoard, 1, updatedBoard)

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
            <input id="BoardId" name="BoardId" type="hidden" defaultValue={props.board.id}></input>
            <h1 className="text-xl font-medium text-ellipsis overflow-hidden whitespace-nowrap">Edit Board - {props.board.name}</h1>
            <p className="flex flex-col gap-y-3">
              <label htmlFor="name">Name</label>
              <input type="text" id="name" name="name" defaultValue={props.board.name}></input>
            </p>
            <p className="flex flex-row gap-4 justify-center">
              <button 
                onClick={closeModal} 
                className="font-medium border-1 border-gray-300 bg-white font-raleway py-2 px-4">
                Cancel
              </button>
              <button
                type="submit" 
                className="text-white font-raleway font-medium text-base bg-main-blue hover:bg-gray-50 hover:text-black hover:border-main-blue border-1 w-full py-2">
                Update
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