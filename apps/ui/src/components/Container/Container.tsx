import React, { forwardRef } from "react";

import { Handle, Remove } from "../Item";

import styles from "./Container.module.css";
import clsx from "clsx";
import { JobList } from "@/types";
import { Button } from "../ui/button";
import Link from "next/link";
import { Plus } from "lucide-react";

export interface Props {
  children: React.ReactNode;
  style?: React.CSSProperties;
  horizontal?: boolean;
  hover?: boolean;
  handleProps?: React.HTMLAttributes<any>;
  scrollable?: boolean;
  shadow?: boolean;
  placeholder?: boolean;
  onClick?(): void;
  onRemove?(): void;
  jobList?: JobList;
  boardId?: string;
}

export const Container = forwardRef<HTMLDivElement, Props>(
  (
    {
      children,
      handleProps,
      horizontal,
      hover,
      onClick,
      onRemove,
      placeholder,
      style,
      scrollable,
      shadow,
      jobList,
      boardId,
      ...props
    }: Props,
    ref
  ) => {
    return (
      <div
        {...props}
        ref={ref}
        style={
          {
            ...style,
            "--columns": 1,
          } as React.CSSProperties
        }
        className={clsx(
          styles.Container,
          horizontal && styles.horizontal,
          hover && styles.hover,
          placeholder && styles.placeholder,
          scrollable && styles.scrollable,
          shadow && styles.shadow,
          "border-r"
        )}
        onClick={onClick}
        tabIndex={onClick ? 0 : undefined}
      >
        {jobList ? (
          <div className="flex w-full flex-col gap-y-4 border-b border-gray-300 bg-white p-4">
            <p className="truncate whitespace-nowrap text-base font-medium">
              {jobList.name}
            </p>
            <p>{jobList.jobs.length} Jobs</p>
            <Button variant="default" asChild>
              <Link
                href={`/track/create-job/${boardId}/${jobList.id}`}
                className="flex flex-row items-center gap-x-2 rounded-full"
              >
                <Plus className="h-4 w-4" />
                <p>Add Job</p>
              </Link>
            </Button>
            <div className="ml-auto flex w-max flex-row gap-x-2">
              {onRemove && <Remove onClick={onRemove} />}
              <Handle {...handleProps} />
            </div>
          </div>
        ) : null}
        {placeholder ? (
          children
        ) : (
          <ul className="grid grid-cols-[repeat(var(--columns,1),1fr)] gap-y-4 p-4">
            {children}
          </ul>
        )}
      </div>
    );
  }
);

Container.displayName = "Container";
