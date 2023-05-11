"use client";

import { useReducer, useState } from "react";
import { postAsync } from "@/lib/serverFetch";
import reducer from "reducers/CreateJobReducer";
import { Job } from "types";
import { BoardDictionaryResponse } from "types/responses/Board";
import { ActionButton, ModalContainer } from "../Common";
import ColourPicker from "../Common/ColourPicker";
import Input from "../Common/Input";
import Select from "../Common/Select";
import { useRouter } from "next/navigation";
import { useAuth } from "@clerk/nextjs";

interface Props {
  boardId: string;
  jobListId: string;
  boardsDictionary: BoardDictionaryResponse[];
}

type Option = {
  value: string;
  label: string;
};

export const CreateJobModal = ({
  boardId,
  jobListId,
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
  const router = useRouter();
  const { getToken } = useAuth();

  const [selectedBoard, setSelectedBoard] = useState<string>(boardId);

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    const createdJob = await postAsync<
      Pick<Job, "company" | "title" | "jobListId" | "boardId">,
      Job
    >("/Job/Create", state.job, {
      headers: {
        Authorization: `Bearer ${await getToken()}`,
      },
    });

    if (state.job.boardId === boardId) {
      // setContainerDict((containerDict) => {
      //   return {
      //     ...containerDict,
      //     [jobListId]: {
      //       ...containerDict[jobListId],
      //       jobs: [...containerDict[jobListId].jobs, createdJob],
      //     },
      //   };
      // });
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

    const board = boardsDictionary.find((board) => board.id === value);

    if (board) {
      dispatch({
        type: "HANDLE_INPUT_CHANGE",
        name: "boardId",
        value: value,
      });

      dispatch({
        type: "HANDLE_INPUT_CHANGE",
        name: "jobListId",
        value: board.jobLists?.[0]?.id ?? "",
      });
    }
  };

  const jobListOptions = (): Option[] => {
    const board = boardsDictionary.find((board) => board.id === boardId);

    const jobLists = board?.jobLists.map((jobList) => ({
      value: jobList.id,
      label: jobList.name,
    }));

    if (!jobLists) return [];

    const index = jobLists.findIndex((jobList) => jobList.value === jobListId);

    if (index !== -1) {
      const [selectedJobList] = jobLists.splice(index, 1);
      return [selectedJobList, ...jobLists];
    }
    return jobLists;
  };

  const boardOptions = (): Option[] => {
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
  };

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
        <ColourPicker
          onChange={(value) => {
            dispatch({
              type: "HANDLE_INPUT_CHANGE",
              name: "colour",
              value: value,
            });
          }}
        />
        <Input name="boardId" type="hidden" value={selectedBoard} />
        <Input name="jobListId" type="hidden" value={jobListId} />
        <div className="flex flex-row gap-4">
          <Select
            name="boardId"
            label="Board"
            options={boardOptions()}
            onChange={(value) => {
              handleBoardChange(value);
            }}
          />
          <Select
            name="jobListId"
            label="List"
            options={jobListOptions()}
            onChange={(value) => {
              dispatch({
                type: "HANDLE_INPUT_CHANGE",
                name: "jobListId",
                value: value,
              });
            }}
          />
        </div>
        <div className="flex flex-row justify-center gap-4">
          <ActionButton
            variant="secondary"
            text="Cancel"
            onClick={() => router.back()}
            type="button"
          />
          <ActionButton
            variant="primary"
            text="Create"
            type="submit"
            extended
          />
        </div>
      </form>
    </ModalContainer>
  );
};
