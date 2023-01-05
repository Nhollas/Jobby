import React, { forwardRef } from "react";
import classNames from "classnames";

import { Handle, Remove } from "../Item";

import styles from "./container.module.css";
import { JobList } from "../../types";

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
          <div className={styles.Header}>
            {list.name}
            <div className={styles.Actions}>
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
