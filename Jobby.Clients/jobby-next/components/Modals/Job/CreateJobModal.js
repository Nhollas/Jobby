import { createJob } from "../../../services/job/jobService"

export const CreateJobModal = (props) => {

  const handleSubmit = async (event) => {
    event.preventDefault()

    //TODO: Validation with YUP package.

    var job = {
      company: event.target.company.value,
      title: event.target.title.value,
      boardId: event.target.boardId.value,
      jobListId: event.target.jobListId.value
    }
    
    var createdJob = await createJob(job)

    const selectedJobListItem = props.board.jobList.find((obj) => obj.id == props.jobList.id)

    selectedJobListItem.jobs.push(createdJob);

    props.boardState(props.board)

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
          <form
            onSubmit={handleSubmit} 
            className="flex flex-col gap-y-8" 
            method="post">
            <h1 className="text-xl font-medium">Create Job</h1>
            <p className="flex flex-col gap-y-2">
              <label htmlFor="title">Title</label>
              <input id="title" name="title"></input>
            </p>
            <p className="flex flex-col gap-y-2">
              <label htmlFor="company">Company</label>
              <input id="company" name="company"></input>
            </p>
            <p className="flex flex-col gap-y-2">
              <label htmlFor="boardName">Board</label>
              <input disabled id="boardName" name="boardName" defaultValue={props.board.name}></input>
            </p>
            <p className="flex flex-col gap-y-2">
              <label htmlFor="jobListName">JobList</label>
              <input disabled id="jobListName" name="jobListName" defaultValue={props.jobList.name}></input>
            </p>
            <input id="boardId" name="boardId" defaultValue={props.board.id} type="hidden"></input>
            <input id="jobListId" name="jobListId" defaultValue={props.jobList.id} type="hidden"></input>
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