// kanbanUtils.ts

import { JobListPreview } from "@/types";
import { Active, defaultDropAnimationSideEffects, DropAnimation, Over } from "@dnd-kit/core";
import { arrayMove } from "@dnd-kit/sortable";
import { Dispatch, SetStateAction } from "react";

type ContainerDict = Record<string, JobListPreview>;



export async function handleContainerMove(
  active: Active,
  over: Over,
  containerKeys: string[],
  setContainerKeys: React.Dispatch<React.SetStateAction<string[]>>
) {
  const activeId = active.id as string;
  const overId = over.id as string;

  const activeIndex = containerKeys.indexOf(activeId);
  const overIndex = containerKeys.indexOf(overId);

  const updatedJobLists = arrayMove(containerKeys, activeIndex, overIndex);

  // Need to post this to the server.
  const jobListIndexes = updatedJobLists.reduce(
    (acc: { [key: string]: number }, list) => {
      acc[list] = updatedJobLists.indexOf(list);
      return acc;
    },
    {}
  );

  setContainerKeys(updatedJobLists);
}

export async function handleJobMove(
  active: Active,
  activeContainer: string,
  over: Over | null,
  containerDict: ContainerDict,
  setContainerDict: Dispatch<SetStateAction<Record<string, JobListPreview>>>
) {
  const overId = over?.id as string;

  const overContainer = findContainer(overId, containerDict);
  

  if (!overContainer) {
    return undefined;
  }

  const activeIndex = containerDict[activeContainer].jobs.findIndex(
    (j) => j.id === active.id
  );
  const overIndex = containerDict[overContainer].jobs.findIndex(
    (j) => j.id === overId
  );

  let jobIndexes = {};

  if (activeIndex !== overIndex) {
    const updatedJobs = arrayMove(
      containerDict[overContainer].jobs,
      activeIndex,
      overIndex
    );

    jobIndexes = updatedJobs.reduce(
      (acc: { [key: string]: number }, job) => {
        acc[job.id] = updatedJobs.indexOf(job);
        return acc;
      },
      {}
    );

    setContainerDict((containerDict) => {
      {
        return {
          ...containerDict,
          [overContainer]: {
            ...containerDict[overContainer],
            jobs: updatedJobs,
          },
        };
      }
    });

    return {
      jobListId: overContainer,
      jobIndexes,
    };
  } else {
    jobIndexes = containerDict[overContainer].jobs.reduce(
      (acc: { [key: string]: number }, job) => {
        acc[job.id] = containerDict[overContainer].jobs.indexOf(job);
        return acc;
      },
      {}
    );

    return {
      jobListId: overContainer,
      jobIndexes,
    };
  }
}

export function findContainer(id: string, containerDict: ContainerDict) {
    if (id in containerDict) {
    return id;
    }
  
      return Object.keys(containerDict).find((key) =>
      containerDict[key].jobs.some((job) => job.id === id)
    );
}

export const dropAnimation: DropAnimation = {
    sideEffects: defaultDropAnimationSideEffects({
      styles: {
        active: {
          opacity: "0.5",
        },
      },
    }),
};

const defaultInitializer = (index: number) => index;

export function createRange<T = number>(
  length: number,
  initializer: (index: number) => any = defaultInitializer
): T[] {
  return [...new Array(length)].map((_, index) => initializer(index));
}


