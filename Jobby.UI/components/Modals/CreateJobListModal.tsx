import { ActionButton, ModalContainer } from "../Common";
import { JobList } from "../../types";
import { Dispatch, SetStateAction, useState } from "react";
import { CreateJobListRequest } from "../../types/requests/JobList";
import { client } from "../../clients";

interface Props {
  setShowCreateJobListModal: Dispatch<
    SetStateAction<{
      visible: boolean;
      boardId?: string | null;
      setContainerDict?: Dispatch<SetStateAction<Record<string, JobList>>>;
      setContainerKeys?: Dispatch<SetStateAction<string[]>>;
      setActiveId?: Dispatch<SetStateAction<string>>;
      activeId?: string | null;
      containerKeys?: string[] | null;
      tempId?: string | null;
    }>
  >;
  showCreateJobListModal: {
    visible: boolean;
    boardId?: string | null;
    setContainerDict?: Dispatch<SetStateAction<Record<string, JobList>>>;
    setContainerKeys?: Dispatch<SetStateAction<string[]>>;
    setActiveId?: Dispatch<SetStateAction<string>>;
    activeId?: string | null;
    containerKeys?: string[] | null;
    tempId?: string | null;
  };
}

export const CreateJobListModal = ({
  setShowCreateJobListModal,
  showCreateJobListModal,
}: Props) => {
  const {
    visible,
    setContainerDict,
    setContainerKeys,
    setActiveId,
    boardId,
    activeId,
    containerKeys,
    tempId,
  } = showCreateJobListModal;

  const [name, setName] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    const request: CreateJobListRequest = {
      boardId,
      name,
      index: containerKeys.length + 1,
      initJobId: activeId,
    };

    const createdJobList = await client.post<CreateJobListRequest, JobList>(
      "/JobList/Create",
      request
    );

    setContainerKeys((prevKeys) =>
      [...prevKeys, createdJobList.id].filter((key) => key !== tempId)
    );
    setContainerDict((prevDict) => {
      const jobToMove = Object.values(prevDict)
        .flatMap((list) => list.jobs)
        .find((j) => j.id === activeId);

      const { [tempId]: removed, ...newDict } = prevDict;

      return {
        ...newDict,
        [jobToMove.jobListId]: {
          ...prevDict[jobToMove.jobListId],
          jobs: [
            ...prevDict[jobToMove.jobListId].jobs.filter(
              (j) => j.id !== jobToMove.id
            ),
          ],
        },
        [createdJobList.id]: {
          ...createdJobList,
          jobs: [{ ...jobToMove, jobListId: createdJobList.id }],
        },
      };
    });
    setActiveId(null);
    setShowCreateJobListModal({ visible: false });
  };

  if (visible) {
    return (
      <ModalContainer>
        <form
          onSubmit={handleSubmit}
          className='flex flex-col gap-y-8'
          method='post'
        >
          <h1 className='text-xl font-medium'>Create Job List</h1>
          <p className='flex flex-col gap-y-2'>
            <label htmlFor='name'>Name</label>
            <input
              id='name'
              name='name'
              className='border px-3 py-1.5 '
              value={name}
              onChange={(e) => setName(e.target.value)}
            />
          </p>
          <p className='flex flex-row justify-center gap-4'>
            <ActionButton
              variant='secondary'
              text='Cancel'
              onClick={() =>
                setShowCreateJobListModal({
                  visible: false,
                })
              }
            />
            <ActionButton
              variant='primary'
              text='Create'
              type='submit'
              extended
            />
          </p>
        </form>
      </ModalContainer>
    );
  }
};
