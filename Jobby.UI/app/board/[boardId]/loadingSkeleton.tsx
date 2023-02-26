"use client";

import { SortableContext } from "@dnd-kit/sortable";
import classNames from "classnames";
import styles from "../../../components/Container/Container.module.css";
import itemstyles from "../../../components/Item/Item.module.css";

export const BoardSkeleton = () => {
  const containerKeys = ["1", "2", "3", "4", "5"];

  const containerDict = {
    "1": {
      id: 1,
      jobs: [
        {
          id: 1,
        },
        {
          id: 2,
        },
        {
          id: 3,
        },
      ],
    },
    "2": {
      id: 2,
      jobs: [
        {
          id: 1,
        },
        {
          id: 2,
        },
      ],
    },
    "3": {
      id: 3,
      jobs: [
        {
          id: 1,
        },
      ],
    },
    "4": {
      id: 4,
      jobs: [],
    },
    "5": {
      id: 5,
      jobs: [
        {
          id: 1,
        },
        {
          id: 2,
        },
        {
          id: 3,
        },
      ],
    },
  };

  return (
    <div className='h-full overflow-x-auto border border-gray-300'>
      <div className='flex h-full divide-x divide-x-reverse'>
        <SortableContext items={[...containerKeys]}>
          {containerKeys.map((containerId) => {
            return (
              <ContainerSkeleton
                key={containerId}
                items={containerDict[containerId].jobs}
              />
            );
          })}
        </SortableContext>
      </div>
    </div>
  );
};

const ContainerSkeleton = ({ items }) => {
  return (
    <div
      style={
        {
          "--columns": 1,
        } as React.CSSProperties
      }
      className={classNames(styles.Container)}
    >
      <div className='flex w-full flex-col gap-y-4 border-b border-gray-300 bg-white p-4'>
        <h1 className='h-5 w-1/4 animate-pulse rounded-md bg-gray-500'></h1>
        <p className='h-5 w-1/5 animate-pulse rounded-md bg-gray-300'></p>
        <div className='ml-auto h-10 w-1/2 animate-pulse rounded-full bg-main-blue'></div>
        <div className='ml-auto h-8 w-6 animate-pulse rounded-md bg-gray-200'></div>
      </div>
      <ul className='flex h-full flex-col gap-y-4 p-4'>
        {items.map((item) => (
          <JobSkeleton key={item.id} />
        ))}
      </ul>
    </div>
  );
};

const JobSkeleton = () => {
  return (
    <li className={classNames(itemstyles.Wrapper)}>
      <div
        className={classNames(
          itemstyles.Item,
          "flex w-full flex-col gap-y-4 bg-white"
        )}
      >
        <h1 className='h-5 w-3/4 animate-pulse rounded-md bg-gray-500'></h1>
        <h2 className='h-5 w-1/2 animate-pulse rounded-md bg-gray-300'></h2>
        <p className='ml-auto h-5 w-1/2 animate-pulse rounded-md bg-gray-300'></p>
      </div>
    </li>
  );
};
