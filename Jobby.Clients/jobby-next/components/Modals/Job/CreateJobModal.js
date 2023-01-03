import { createJob } from "../../../services/jobService";
import ModalContainer from "../../Common/ModalContainer";

export const CreateJobModal = (props) => {
  const { setCurrentBoard, setShowCreateModal, showCreateModal, accessToken } =
    props;

  const { visible, jobList, board } = showCreateModal;

  const handleSubmit = async (event) => {
    event.preventDefault();

    //TODO: Validation with YUP package.

    var job = {
      company: event.target.company.value,
      title: event.target.title.value,
      boardId: event.target.boardId.value,
      jobListId: event.target.jobListId.value,
    };

    var createdJob = await createJob(job, accessToken);

    setCurrentBoard((prevBoard) => {
      const selectedJobListItem = prevBoard.jobList.find(
        (obj) => obj.id == jobList.id
      );
      selectedJobListItem.jobs = [...selectedJobListItem.jobs, createdJob];
      return prevBoard;
    });

    setShowCreateModal({
      visible: false,
      board: null,
      jobList: null,
    });
  };

  if (visible) {
    return (
      <ModalContainer>
        <form
          onSubmit={handleSubmit}
          className='flex flex-col gap-y-8'
          method='post'
        >
          <h1 className='text-xl font-medium'>Create Job</h1>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='title'>Title</label>
            <input id='title' name='title'></input>
          </p>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='company'>Company</label>
            <input id='company' name='company'></input>
          </p>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='boardName'>Board</label>
            <input
              disabled
              id='boardName'
              name='boardName'
              defaultValue={board.name}
            ></input>
          </p>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='jobListName'>JobList</label>
            <input
              disabled
              id='jobListName'
              name='jobListName'
              defaultValue={jobList.name}
            ></input>
          </p>
          <input
            id='boardId'
            name='boardId'
            defaultValue={board.id}
            type='hidden'
          ></input>
          <input
            id='jobListId'
            name='jobListId'
            defaultValue={jobList.id}
            type='hidden'
          ></input>
          <p className='flex flex-row justify-center gap-4'>
            <button
              onClick={() =>
                setShowCreateModal({
                  visible: false,
                  board: null,
                  jobList: null,
                })
              }
              className='font-raleway border border-gray-300 bg-white py-2 px-4 font-medium'
            >
              Cancel
            </button>
            <button
              type='submit'
              className='font-raleway w-full border bg-main-blue py-2 text-base font-medium text-white hover:border-main-blue hover:bg-gray-50 hover:text-black'
            >
              Create
            </button>
          </p>
        </form>
      </ModalContainer>
    );
  }
};
