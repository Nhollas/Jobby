import React, { useEffect } from "react";
import classNames from "classnames";
import type { Transform } from "@dnd-kit/utilities";

import styles from "./Item.module.css";
import { Job } from "../../types";
import { DraggableSyntheticListeners } from "@dnd-kit/core";

export interface Props {
  dragOverlay?: boolean;
  disabled?: boolean;
  index?: number;
  job: Job;
  transition?: string | null;
  transform?: Transform | null;
  dragging?: boolean;
  sorting?: boolean;
  fadeIn?: boolean;
  listeners?: DraggableSyntheticListeners;
}

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
          className={classNames(
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
              "--index": index
            } as React.CSSProperties
          }
          ref={ref}
        >
          <div
            className={classNames(
              styles.Item,
              "flex flex-col gap-y-2 w-full",
              dragging && styles.dragging,
              dragOverlay && styles.dragOverlay,
              disabled && styles.disabled
            )}
            data-cypress="draggable-item"
            {...listeners}
            {...props}
            tabIndex={0}
          >
            <h1 className="text-lg font-medium">{job.title}</h1>
            <h2>{job.company}</h2>
            <p className="text-sm ml-auto">{job.createdDate}</p>
          </div>
        </li>
      );
    }
  )
);
