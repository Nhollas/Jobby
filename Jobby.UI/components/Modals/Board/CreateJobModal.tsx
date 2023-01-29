import {
  Dispatch,
  SetStateAction,
  useContext,
  useReducer,
  useState,
} from "react";
import { client } from "../../../clients";
import { ModalContext } from "../../../contexts/ModalContext";
import reducer from "../../../reducers/CreateJobReducer";
import { Job, JobList } from "../../../types";
import { BoardDictionaryResponse } from "../../../types/responses/Board";
import { ActionButton, ModalContainer } from "../../Common";
import ColourPicker from "../../Form/ColourPicker";
import Input from "../../Form/Input";
import Select from "../../Form/Select";

interface Props {
  setShowCreateJobModal: Dispatch<
    SetStateAction<{
      visible: boolean;
      boardId?: string | null;
      jobListId: string | null;
      setContainerDict?: Dispatch<SetStateAction<Record<string, JobList>>>;
    }>
  >;
  showCreateJobModal: {
    visible: boolean;
    boardId: string | null;
    jobListId: string | null;
    setContainerDict?: Dispatch<SetStateAction<Record<string, JobList>>>;
  };
  boardsDictionary: BoardDictionaryResponse[];
}

export const CreateJobModal = ({
  setShowCreateJobModal,
  showCreateJobModal,
  boardsDictionary,
}: Props) => {
  const { visible, jobListId, boardId, setContainerDict } = showCreateJobModal;

  if (!visible) {
    return;
  }

  const { setShowViewJobModal } = useContext(ModalContext);

  const [state, dispatch] = useReducer(reducer, {
    job: {
      title: "",
      company: "",
      colour: "#2820ca",
      jobListId,
      boardId,
    },
  });

  const [selectedBoard, setSelectedBoard] = useState<string>(boardId);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const createdJob = await client.post<
      Pick<Job, "company" | "title" | "jobListId" | "boardId">,
      Job
    >("/Job/Create", state.job);

    if (state.job.boardId === boardId) {
      setContainerDict((containerDict) => {
        return {
          ...containerDict,
          [jobListId]: {
            ...containerDict[jobListId],
            jobs: [...containerDict[jobListId].jobs, createdJob],
          },
        };
      });
    }

    const fetchedJob = await client.get<Job>(
      `Board/${createdJob.boardId}/Job/${createdJob.id}`
    );

    setShowCreateJobModal({
      visible: false,
      boardId: null,
      jobListId: null,
    });

    setShowViewJobModal({
      visible: true,
      job: fetchedJob,
    });
  };

  const handleChange = (e) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: e.target.name,
      value: e.target.value,
    });
  };

  const handleBoardChange = (value) => {
    setSelectedBoard(value);
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: "boardId",
      value: value,
    });

    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: "jobListId",
      value: boardsDictionary.find((board) => board.id === value).jobLists[0]
        .id,
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
          <Input
            name='title'
            label='Title'
            onChange={handleChange}
            type='text'
            value={state.job.title}
          />
          <Input
            name='company'
            label='Company'
            onChange={handleChange}
            type='text'
            value={state.job.company}
          />
          <ColourPicker
            onChange={(value) => {
              dispatch({
                type: "HANDLE_INPUT_CHANGE",
                name: "colour",
                value: value,
              });
            }}
          />
          <Input name='boardId' type='hidden' value={selectedBoard} />
          <Input name='jobListId' type='hidden' value={jobListId} />
          <div className='flex flex-row gap-4'>
            <Select
              name='boardId'
              label='Board'
              options={boardsDictionary.map((board) => ({
                value: board.id,
                label: board.name,
              }))}
              onChange={(value) => {
                handleBoardChange(value);
              }}
            />
            <Select
              name='jobListId'
              label='List'
              options={boardsDictionary
                .find((board) => board.id === selectedBoard)
                ?.jobLists.map((jobList: JobList) => ({
                  value: jobList.id,
                  label: jobList.name,
                }))}
              onChange={(value) => {
                dispatch({
                  type: "HANDLE_INPUT_CHANGE",
                  name: "jobListId",
                  value: value,
                });
              }}
            />
          </div>
          <div className='flex flex-row justify-center gap-4'>
            <ActionButton
              variant='secondary'
              text='Cancel'
              onClick={() =>
                setShowCreateJobModal({
                  visible: false,
                  jobListId: null,
                  boardId: null,
                })
              }
            />
            <ActionButton
              variant='primary'
              text='Create'
              type='submit'
              extended
            />
          </div>
        </form>
      </ModalContainer>
    );
  }
};
