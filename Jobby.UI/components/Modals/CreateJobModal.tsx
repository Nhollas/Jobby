"use client";

import {
  Dispatch,
  SetStateAction,
  useContext,
  useReducer,
  useState,
} from "react";
import { client } from "clients";
import reducer from "reducers/CreateJobReducer";
import { Job, JobList, JobListPreview } from "../../types";
import { BoardDictionaryResponse } from "types/responses/Board";
import { ActionButton } from "../Common";
import ColourPicker from "../Common/ColourPicker";
import Input from "../Common/Input";
import Select from "../Common/Select";

interface Props {
  boardId: string;
  jobListId: string;
  setContainerDict: Dispatch<SetStateAction<Record<string, JobListPreview>>>;
  boardsDictionary: BoardDictionaryResponse[];
}

export const CreateJobModal = ({
  boardId,
  jobListId,
  setContainerDict,
  boardsDictionary,
}: Props) => {
  const [state, dispatch] = useReducer(reducer, {
    job: {
      title: "",
      company: "",
      colour: "#ffffff",
      jobListId,
      boardId,
    },
  });

  const [selectedBoard, setSelectedBoard] = useState<string>(boardId);

  const handleSubmit = async (e: any) => {
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
  };

  const handleChange = (e: any) => {
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: e.target.name,
      value: e.target.value,
    });
  };

  const handleBoardChange = (value: string) => {
    setSelectedBoard(value);
    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: "boardId",
      value: value,
    });

    const board = boardsDictionary.find((board) => board.id === value);

    dispatch({
      type: "HANDLE_INPUT_CHANGE",
      name: "jobListId",
      value: board?.jobLists[0].id || jobListId,
    });
  };

  return (
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
          options={(() => {
            const boards = boardsDictionary.map((board) => ({
              value: board.id,
              label: board.name,
            }));
            const index = boards.findIndex((board) => board.value === boardId);
            if (index !== -1) {
              const [selectedBoard] = boards.splice(index, 1);
              return [selectedBoard, ...boards];
            }
            return boards;
          })()}
          onChange={(value) => {
            handleBoardChange(value);
          }}
        />
        <Select
          name='jobListId'
          label='List'
          options={(() => {
            const selectedBoardObj = boardsDictionary.find(
              (board) => board.id === selectedBoard
            );
            const jobLists = selectedBoardObj
              ? selectedBoardObj.jobLists.map((jobList) => ({
                  value: jobList.id,
                  label: jobList.name,
                }))
              : [];

            const index = jobLists.findIndex(
              (jobList) => jobList.value === jobListId
            );

            if (index !== -1) {
              const [selectedJobList] = jobLists.splice(index, 1);
              return [selectedJobList, ...jobLists];
            }

            return jobLists;
          })()}
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
          onClick={() => console.log("cancel")}
        />
        <ActionButton variant='primary' text='Create' type='submit' extended />
      </div>
    </form>
  );
};
