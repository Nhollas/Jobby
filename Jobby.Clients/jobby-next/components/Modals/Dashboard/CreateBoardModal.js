import { createBoard } from "../../../services/board/boardService"

export const CreateBoardModal = (props) => {

  const handleSubmit = async (event) => {
    event.preventDefault()

    //TODO: Validation with YUP package.

    var board = {
      name: event.target.name.value
    }
    
    var createdBoard = await createBoard(board)

    // Add newly created board to existing list rather than having to do another fetch.
    props.boardList.push(createdBoard);

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
      <div className="absolute inset-0 top-8 w-full h-full flex justify-center bg-white/90">
        <div className="w-full max-w-md border-1 h-max border-gray-300 p-6 bg-gray-50">
          <form
            onSubmit={handleSubmit} 
            className="flex flex-col gap-y-8" 
            method="post">
            <h1 className="text-xl font-medium">Create Board</h1>
            <p className="flex flex-col gap-y-2">
              <label htmlFor="name"></label>
              <input id="name" name="name"></input>
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
                Create
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