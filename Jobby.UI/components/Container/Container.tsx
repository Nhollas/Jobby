import React, { Dispatch, forwardRef, SetStateAction } from "react";
import classNames from "classnames";

import { Handle, Remove } from "../Item";

import styles from "./container.module.css";
import { JobList } from "../../types";
import { ActionButton } from "../Common";

export interface Props {
  children: React.ReactNode;
  style?: React.CSSProperties;
  hover?: boolean;
  handleProps?: React.HTMLAttributes<any>;
  shadow?: boolean;
  placeholder?: boolean;
  list: JobList;
  onClick?(): void;
  onRemove?(): void;
  setShowCreateJobModal: Dispatch<
    SetStateAction<{
      visible: boolean;
      boardId?: string | null;
      jobListId: string | null;
      setContainerDict?: Dispatch<SetStateAction<Record<string, JobList>>>;
    }>
  >;
  setContainerDict: Dispatch<SetStateAction<Record<string, JobList>>>;
  boardId: string;
}

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
      setShowCreateJobModal,
      setContainerDict,
      boardId,
      ...props
    }: Props,
    ref
  ) => {
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
          shadow && styles.shadow
        )}
        onClick={onClick}
        tabIndex={onClick && 0}
      >
        {list ? (
          <div className="flex w-full flex-col gap-y-4 border-b border-gray-300 bg-white p-4">
            <p className="truncate whitespace-nowrap text-base font-medium">
              {list.name}
            </p>
            <p>{list.jobs.length} Jobs</p>
            <ActionButton
              variant="primary"
              text="Add Job"
              rounded
              className="ml-auto"
              onClick={() =>
                setShowCreateJobModal({
                  visible: true,
                  jobListId: list.id,
                  boardId,
                  setContainerDict,
                })
              }
            />
            <div className="ml-auto flex w-max flex-row gap-x-2">
              {onRemove && <Remove onClick={onRemove} />}
              <Handle {...handleProps} />
            </div>
          </div>
        ) : null}
        {placeholder ? (
          children
        ) : (
          <ul className="flex h-full flex-col gap-y-4 p-4">{children}</ul>
        )}
      </Component>
    );
  }
);
