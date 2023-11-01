import React, { useEffect } from "react";
import type { DraggableSyntheticListeners } from "@dnd-kit/core";
import type { Transform } from "@dnd-kit/utilities";

import styles from "./Item.module.css";
import clsx from "clsx";
import { Job } from "@/types";
import Link from "next/link";

export interface Props {
  dragOverlay?: boolean;
  disabled?: boolean;
  dragging?: boolean;
  handleProps?: any;
  height?: number;
  index?: number;
  fadeIn?: boolean;
  transform?: Transform | null;
  listeners?: DraggableSyntheticListeners;
  sorting?: boolean;
  transition?: string | null;
  job: Job;
}

export const Item = React.memo(
  React.forwardRef<HTMLLIElement, Props>(
    (
      {
        dragOverlay,
        dragging,
        disabled,
        fadeIn,
        handleProps,
        height,
        index,
        listeners,
        sorting,
        transition,
        transform,
        job,
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
            dragOverlay && styles.dragOverlay
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
            } as React.CSSProperties
          }
          ref={ref}
        >
          <Link
            href={`/track/board/${job.boardReference}/job/${job.reference}/info`}
            className={clsx(
              styles.Item,
              "flex w-full flex-col gap-y-2",
              dragging && styles.dragging,
              dragOverlay && styles.dragOverlay,
              disabled && styles.disabled
            )}
            data-cypress="draggable-item"
            {...listeners}
            {...props}
            tabIndex={0}
          >
            <h1 className="w-full truncate text-lg font-medium">{job.title}</h1>
            <h2 className="">{job.company}</h2>
            <p className="ml-auto w-max text-sm">
              {new Date(job.createdDate).toDateString()}
            </p>
          </Link>
        </li>
      );
    }
  )
);
