"use client";

import React, { Dispatch, forwardRef, SetStateAction } from "react";
import classNames from "classnames";

import styles from "./Container.module.css";
import { Handle, Remove } from "../Item";
import { JobListPreview } from "types";
import { ActionButton } from "../../Common";
import Link from "next/link";
import { Button } from "@/components/ui/button";
import { Plus } from "lucide-react";

export interface Props {
  children: React.ReactNode;
  style?: React.CSSProperties;
  hover?: boolean;
  handleProps?: React.HTMLAttributes<any>;
  shadow?: boolean;
  placeholder?: boolean;
  list?: JobListPreview;
  onClick?(): void;
  onRemove?: () => Promise<void>;
  setContainerDict: Dispatch<SetStateAction<Record<string, JobListPreview>>>;
  boardId: string;
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
          shadow && styles.shadow,
          "last:border-r"
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
            <Button variant="default" asChild>
              <Link
                href={`/create-job/${boardId}/${list.id}`}
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
          <ul className="flex h-full flex-col gap-y-4 p-4">{children}</ul>
        )}
      </Component>
    );
  }
);
