import React, { useEffect, useState } from "react";
import { useSortable } from "@dnd-kit/sortable";
import { Item } from "../../Item";
import { Job } from "types";

function useMountStatus() {
  const [isMounted, setIsMounted] = useState(false);

  useEffect(() => {
    const timeout = setTimeout(() => setIsMounted(true), 500);

    return () => clearTimeout(timeout);
  }, []);

  return isMounted;
}

interface SortableItemProps {
  id: string;
  index: number;
  disabled?: boolean;
  job: Job;
  loading?: boolean;
}

export function SortableItem({
  disabled,
  id,
  index,
  job,
  loading,
}: SortableItemProps) {
  const {
    setNodeRef,
    listeners,
    isDragging,
    isSorting,
    transform,
    transition,
  } = useSortable({
    id,
  });
  const mounted = useMountStatus();
  const mountedWhileDragging = isDragging && !mounted;

  return (
    <Item
      ref={disabled ? undefined : setNodeRef}
      job={job}
      dragging={isDragging}
      sorting={isSorting}
      index={index}
      transition={transition}
      transform={transform}
      fadeIn={mountedWhileDragging}
      listeners={listeners}
      loading={loading}
    />
  );
}
