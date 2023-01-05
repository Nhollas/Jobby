import { Container, ContainerProps } from "../../../../components";
import {
  useSortable,
  AnimateLayoutChanges,
  defaultAnimateLayoutChanges
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { Job, JobList } from "../../../../types";
import { Dispatch } from "react";

const animateLayoutChanges: AnimateLayoutChanges = (args) =>
  defaultAnimateLayoutChanges({ ...args, wasDragging: true });

export function DroppableContainer({
  children,
  disabled,
  id,
  items,
  style,
  setContainerDict,
  ...props
}: ContainerProps & {
  disabled?: boolean;
  id: string;
  items: Job[];
  style?: React.CSSProperties;
  setContainerDict?: Dispatch<Record<string, JobList>>
}) {
  const {
    active,
    attributes,
    isDragging,
    listeners,
    over,
    setNodeRef,
    transition,
    transform
  } = useSortable({
    id,
    data: {
      type: "container",
      children: items
    },
    animateLayoutChanges
  });
  const isOverContainer = over
    ? (id === over.id && active?.data.current?.type !== "container") ||
      items.some((job) => job.id === over.id)
    : false;

  return (
    <Container
      ref={disabled ? undefined : setNodeRef}
      style={{
        ...style,
        transition,
        transform: CSS.Translate.toString(transform),
        opacity: isDragging ? 0.5 : undefined
      }}
      hover={isOverContainer}
      items={items}
      setContainerDict={setContainerDict}
      handleProps={{
        ...attributes,
        ...listeners
      }}
      {...props}
    >
      {children}
    </Container>
  );
}
