import { Dispatch, SetStateAction, useReducer } from "react";
import { client } from "../../../clients";
import reducer from "../../../reducers/CreateJobReducer";
import { Job, JobList } from "../../../types";
import { ActionButton, ModalContainer } from "../../Common";
import Input from "../../Form/Input";

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
}

export const CreateJobModal = ({
  setShowCreateJobModal,
  showCreateJobModal,
}: Props) => {
  const { visible, jobListId, boardId, setContainerDict } = showCreateJobModal;

  if (!visible) {
    return;
  }

  const [state, dispatch] = useReducer(reducer, {
    job: {
      title: "",
      company: "",
      colour: "#2820ca",
      jobListId,
      boardId,
    },
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    const createdJob = await client.post<
      Pick<Job, "company" | "title" | "jobListId" | "boardId">,
      Job
    >("/Job/Create", state.job);

    setContainerDict((containerDict) => {
      return {
        ...containerDict,
        [jobListId]: {
          ...containerDict[jobListId],
          jobs: [...containerDict[jobListId].jobs, createdJob],
        },
      };
    });

    setShowCreateJobModal({ visible: false, jobListId: null, boardId: null });
  };

  const handleChange = (e) => {
    console.log(e);
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: e.target.name,
      value: e.target.value,
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
          <h1 className="text-xl font-medium">Create Job</h1>
          <Input
            name="title"
            label="Title"
            onChange={handleChange}
            type="text"
            value={state.job.title}
          />
          <Input
            name="company"
            label="Company"
            onChange={handleChange}
            type="text"
            value={state.job.company}
          />
          <Input
            name="colour"
            label="Colour"
            onChange={handleChange}
            type="color"
            value={state.job.colour}
          />
          <Input name="boardId" type="text" hidden value={boardId} />
          <Input name="jobListId" type="text" hidden value={jobListId} />
          <p className="flex flex-row justify-center gap-4">
            <ActionButton
              variant="secondary"
              text="Cancel"
              onClick={() =>
                setShowCreateJobModal({
                  visible: false,
                  jobListId: null,
                  boardId: null,
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
