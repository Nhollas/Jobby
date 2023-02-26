"use client";

import React, { Dispatch, forwardRef, SetStateAction, useContext } from "react";
import classNames from "classnames";

import styles from "./Container.module.css";
import { Handle, Remove } from "../Item";
import { JobList } from "../../types";
import { ActionButton } from "../Common";
import { ModalContext } from "../../contexts/ModalContext";
import { CreateJobModal } from "../Modals/Job/CreateJobModal";
import { BoardDictionaryResponse } from "../../types/responses/Board";

export interface Props {
  children: React.ReactNode;
  style?: React.CSSProperties;
  hover?: boolean;
  handleProps?: React.HTMLAttributes<any>;
  shadow?: boolean;
  placeholder?: boolean;
  list?: JobList;
  onClick?(): void;
  onRemove?(): void;
  setContainerDict: Dispatch<SetStateAction<Record<string, JobList>>>;
  boardId: string;
  boardsDictionary: BoardDictionaryResponse[];
}

// eslint-disable-next-line react/display-name
export const Container = forwardRef<HTMLDivElement & HTMLButtonElement, Props>(
  (
    {
      children,
      style,
      hover,
      handleProps,
      shadow,
      placeholder,
      list,
      onClick,
      onRemove,
      setContainerDict,
      boardId,
      boardsDictionary,
      ...props
    }: Props,
    ref
  ) => {
    const { handleModal } = useContext(ModalContext);

    const Component = onClick ? "button" : "div";

    return (
      <Component
        {...props}
        ref={ref}
        style={
          {
            ...style,
            "--columns": 1,
          } as React.CSSProperties
        }
        className={classNames(
          styles.Container,
          hover && styles.hover,
          placeholder && styles.placeholder,
          shadow && styles.shadow,
          "last:border-r"
        )}
        onClick={onClick}
        tabIndex={onClick && 0}
      >
        {list ? (
          <div className='flex w-full flex-col gap-y-4 border-b border-gray-300 bg-white p-4'>
            <p className='truncate whitespace-nowrap text-base font-medium'>
              {list.name}
            </p>
            <p>{list.jobs.length} Jobs</p>
            <ActionButton
              variant='primary'
              text='Add Job'
              rounded
              className='ml-auto flex items-center gap-x-2'
              icon={<i className='bi bi-plus-lg flex text-xl'></i>}
              onClick={() =>
                handleModal(
                  <CreateJobModal
                    boardId={boardId}
                    jobListId={list.id}
                    setContainerDict={setContainerDict}
                    boardsDictionary={boardsDictionary}
                  />
                )
              }
            />
            <div className='ml-auto flex w-max flex-row gap-x-2'>
              {onRemove && <Remove onClick={onRemove} />}
              <Handle {...handleProps} />
            </div>
          </div>
        ) : null}
        {placeholder ? (
          children
        ) : (
          <ul className='flex h-full flex-col gap-y-4 p-4'>{children}</ul>
        )}
      </Component>
    );
  }
);
