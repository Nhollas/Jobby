"use client";

import React, { useEffect } from "react";
import clsx from "clsx";
import type { Transform } from "@dnd-kit/utilities";

import styles from "./Item.module.css";
import { JobPreview } from "types";
import { DraggableSyntheticListeners } from "@dnd-kit/core";
import Link from "next/link";

export interface Props {
  dragOverlay?: boolean;
  disabled?: boolean;
  index?: number;
  job: JobPreview;
  transition?: string | null;
  transform?: Transform | null;
  dragging?: boolean;
  sorting?: boolean;
  fadeIn?: boolean;
  listeners?: DraggableSyntheticListeners;
  loading?: boolean;
}

type ItemColour = {
  title: string;
  company: string;
  date: string;
  background: string;
};

interface ItemColours {
  [key: string]: ItemColour;
}

const defaultColours: ItemColours = {
  "#ffffff": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#ffffff",
    date: "#6b7280",
  },
  "#ef4444": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#ef4444",
    date: "#6b7280",
  },
  "#f97316": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#f97316",
    date: "#6b7280",
  },
  "#84cc16": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#84cc16",
    date: "#6b7280",
  },
  "#10b981": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#10b981",
    date: "#6b7280",
  },
  "#0ea5e9": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#0ea5e9",
    date: "#6b7280",
  },
  "#3b82f6": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#3b82f6",
    date: "#6b7280",
  },
  "#8b5cf6": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#8b5cf6",
    date: "#6b7280",
  },
  "#d946ef": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#d946ef",
    date: "#6b7280",
  },
  "#f43f5e": {
    title: "#0a0a0a",
    company: "#171717",
    background: "#f43f5e",
    date: "#6b7280",
  },
};

export const Item = React.memo(
  React.forwardRef<HTMLLIElement, Props>(
    (
      {
        job,
        dragOverlay,
        dragging,
        disabled,
        fadeIn,
        index,
        listeners,
        sorting,
        transition,
        transform,
        loading,
        ...props
      },
      ref
    ) => {
      useEffect(() => {
        if (!dragOverlay) {
          return;
        }

        document.body.style.cursor = "grabbing";

        return () => {
          document.body.style.cursor = "";
        };
      }, [dragOverlay]);

      return (
        <li
          className={clsx(
            styles.Wrapper,
            fadeIn && styles.fadeIn,
            sorting && styles.sorting,
            dragOverlay && styles.dragOverlay,
            loading && "animate-pulse"
          )}
          style={
            {
              transition: [transition].filter(Boolean).join(", "),
              "--translate-x": transform
                ? `${Math.round(transform.x)}px`
                : undefined,
              "--translate-y": transform
                ? `${Math.round(transform.y)}px`
                : undefined,
              "--scale-x": transform?.scaleX
                ? `${transform.scaleX}`
                : undefined,
              "--scale-y": transform?.scaleY
                ? `${transform.scaleY}`
                : undefined,
              "--index": index,
              backgroundColor: defaultColours[job.colour].background,
            } as React.CSSProperties
          }
          ref={ref}
        >
          <Link
            href={`/board/${job.boardId}/job/${job.id}/info`}
            className={clsx(
              styles.Item,
              "flex w-full flex-col gap-y-2",
              dragging && styles.dragging,
              dragOverlay && styles.dragOverlay,
              disabled && styles.disabled,
              loading && "pointer-events-none"
            )}
            data-cypress='draggable-item'
            {...listeners}
            {...props}
            tabIndex={0}
          >
            <h1
              className={clsx(
                "w-full truncate text-lg font-medium",
                loading && "rounded-lg bg-gray-300 !text-gray-300"
              )}

            >
              {job.title}
            </h1>
            <h2
              className={clsx("w-max", loading && "rounded-lg bg-gray-100 !text-gray-100")}
 
            >
              {job.company}
            </h2>
            <p
              className={clsx(
                "ml-auto w-max text-sm",
                loading && "rounded-lg bg-gray-100 !text-gray-100"
              )}
            >
              {new Date(job.createdDate).toDateString()}
            </p>
          </Link>
        </li>
      );
    }
  )
);
