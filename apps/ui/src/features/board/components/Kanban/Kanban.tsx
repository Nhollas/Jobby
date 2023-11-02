"use client";

import { useCallback, useEffect, useRef, useState } from "react";
import { createPortal } from "react-dom";
import {
  closestCenter,
  pointerWithin,
  rectIntersection,
  CollisionDetection,
  DndContext,
  DragOverlay,
  DropAnimation,
  getFirstCollision,
  MouseSensor,
  TouchSensor,
  UniqueIdentifier,
  useSensors,
  useSensor,
  MeasuringStrategy,
  defaultDropAnimationSideEffects,
} from "@dnd-kit/core";
import {
  AnimateLayoutChanges,
  useSortable,
  arrayMove,
  defaultAnimateLayoutChanges,
  verticalListSortingStrategy,
  horizontalListSortingStrategy,
  SortableContext,
} from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { Board, Job, JobList } from "@/types";
import { Container, ContainerProps } from "./components/Container";
import { Item } from "./components/Item";

const animateLayoutChanges: AnimateLayoutChanges = (args) =>
  defaultAnimateLayoutChanges({ ...args, wasDragging: true });

function DroppableContainer({
  children,
  disabled,
  id,
  items,
  ...props
}: ContainerProps & {
  disabled?: boolean;
  id: UniqueIdentifier;
  items: UniqueIdentifier[];
}) {
  const {
    active,
    attributes,
    isDragging,
    listeners,
    over,
    setNodeRef,
    transition,
    transform,
  } = useSortable({
    id,
    data: {
      type: "container",
      children: items,
    },
    animateLayoutChanges,
  });
  const isOverContainer = over
    ? (id === over.id && active?.data.current?.type !== "container") ||
      items.includes(over.id)
    : false;

  return (
    <Container
      ref={disabled ? undefined : setNodeRef}
      style={{
        transition,
        transform: CSS.Translate.toString(transform),
        opacity: isDragging ? 0.5 : undefined,
      }}
      hover={isOverContainer}
      handleProps={{
        ...attributes,
        ...listeners,
      }}
      {...props}
    >
      {children}
    </Container>
  );
}

const dropAnimation: DropAnimation = {
  sideEffects: defaultDropAnimationSideEffects({
    styles: {
      active: {
        opacity: "0.5",
      },
    },
  }),
};

interface Props {
  board: Board;
}

export function Kanban({ board }: Props) {
  const [containerDict, setContainerDict] = useState<
    Record<UniqueIdentifier, JobList>
  >(
    board.lists.reduce((acc: Record<UniqueIdentifier, JobList>, list) => {
      acc[list.reference] = list;
      return acc;
    }, {})
  );

  const [containerKeys, setContainerKeys] = useState<UniqueIdentifier[]>(
    Object.keys(containerDict)
  );

  const [activeId, setActiveId] = useState<UniqueIdentifier | null>(null);
  const [moveJob, setMoveJob] = useState<{
    jobId: UniqueIdentifier;
    targetJobListId: UniqueIdentifier;
  } | null>(null);

  const lastOverId = useRef<UniqueIdentifier | null>(null);
  const recentlyMovedToNewContainer = useRef(false);

  const isSortingContainer = activeId ? activeId in containerDict : false;

  /**
   * Custom collision detection strategy optimized for multiple containers
   *
   * - First, find any droppable containers intersecting with the pointer.
   * - If there are none, find intersecting containers with the active draggable.
   * - If there are no intersecting containers, return the last matched intersection
   *
   */
  const collisionDetectionStrategy: CollisionDetection = useCallback(
    (args) => {
      if (activeId && activeId in containerDict) {
        return closestCenter({
          ...args,
          droppableContainers: args.droppableContainers.filter(
            (container) => container.id in containerDict
          ),
        });
      }

      // Start by finding any intersecting droppable
      const pointerIntersections = pointerWithin(args);
      const intersections =
        pointerIntersections.length > 0
          ? // If there are droppables intersecting with the pointer, return those
            pointerIntersections
          : rectIntersection(args);
      let overId = getFirstCollision(intersections, "id");

      if (overId != null) {
        if (overId in containerDict) {
          const containerJobs = containerDict[overId].jobs;

          // If a container is matched and it contains items (columns 'A', 'B', 'C')
          if (containerJobs.length > 0) {
            // Return the closest droppable within that container
            overId = closestCenter({
              ...args,
              droppableContainers: args.droppableContainers.filter(
                (container) =>
                  container.id !== overId &&
                  containerJobs.some((job) => job.reference === container.id)
              ),
            })[0]?.id;
          }
        }

        lastOverId.current = overId;

        return [{ id: overId }];
      }

      // When a draggable item moves to a new container, the layout may shift
      // and the `overId` may become `null`. We manually set the cached `lastOverId`
      // to the id of the draggable item that was moved to the new container, otherwise
      // the previous `overId` will be returned which can cause items to incorrectly shift positions
      if (recentlyMovedToNewContainer.current) {
        lastOverId.current = activeId;
      }

      // If no droppable is matched, return the last match
      return lastOverId.current ? [{ id: lastOverId.current }] : [];
    },
    [activeId, containerDict]
  );
  const [portalElement, setPortalElement] = useState<HTMLDivElement | null>(
    null
  );
  const sensors = useSensors(
    useSensor(MouseSensor, {
      activationConstraint: {
        delay: 250,
        tolerance: 5,
      },
    }),
    useSensor(TouchSensor, {
      activationConstraint: {
        delay: 250,
        tolerance: 5,
      },
    })
  );
  const findContainer = (id: UniqueIdentifier) => {
    if (id in containerDict) {
      return id;
    }

    return Object.keys(containerDict).find((key) =>
      containerDict[key].jobs.some((job) => job.reference === id)
    );
  };

  useEffect(() => {
    const element = document.createElement("div");
    document.body.appendChild(element);
    setPortalElement(element);
    // return () => {
    //   document.body.removeChild(element);
    // };
  }, []);

  useEffect(() => {
    requestAnimationFrame(() => {
      recentlyMovedToNewContainer.current = false;
    });
  }, [containerDict]);

  return (
    <DndContext
      sensors={sensors}
      collisionDetection={collisionDetectionStrategy}
      measuring={{
        droppable: {
          strategy: MeasuringStrategy.Always,
        },
      }}
      onDragStart={({ active }) => {
        setActiveId(active.id);
      }}
      onDragOver={({ active, over }) => {
        const overId = over?.id;

        if (overId == null || active.id in containerDict) {
          return;
        }

        const overContainer = findContainer(overId);
        const activeContainer = findContainer(active.id);

        if (!overContainer || !activeContainer) {
          return;
        }

        if (activeContainer !== overContainer) {
          setContainerDict((prevContainerDict) => {
            const activeJobs = prevContainerDict[activeContainer].jobs;
            const overJobs = prevContainerDict[overContainer].jobs;
            const overIndex = overJobs.findIndex((j) => j.reference === overId);
            const activeIndex = activeJobs.findIndex(
              (j) => j.reference === active.id
            );

            let newIndex: number;

            if (overId in prevContainerDict) {
              newIndex = overJobs.length + 1;
            } else {
              const isBelowOverItem =
                over &&
                active.rect.current.translated &&
                active.rect.current.translated.top >
                  over.rect.top + over.rect.height;

              const modifier = isBelowOverItem ? 1 : 0;

              newIndex =
                overIndex >= 0 ? overIndex + modifier : overJobs.length + 1;
            }

            recentlyMovedToNewContainer.current = true;

            return {
              ...prevContainerDict,
              [activeContainer]: {
                ...prevContainerDict[activeContainer],
                jobs: prevContainerDict[activeContainer].jobs.filter(
                  (job) => job.reference !== active.id
                ),
              },
              [overContainer]: {
                ...prevContainerDict[overContainer],
                jobs: [
                  ...prevContainerDict[overContainer].jobs.slice(0, newIndex),
                  prevContainerDict[activeContainer].jobs[activeIndex],
                  ...prevContainerDict[overContainer].jobs.slice(
                    newIndex,
                    prevContainerDict[overContainer].jobs.length
                  ),
                ].map((job, i) => ({
                  ...job,
                  index: i,
                  jobListId: overContainer as string,
                })),
              },
            };
          });

          var model = {
            jobId: active.id,
            targetJobListId: overContainer,
          };

          setMoveJob(model);
        }
      }}
      onDragEnd={async ({ active, over }) => {
        if (moveJob) {
          // await clientApi.put("job/move", moveJob);

          setMoveJob(null);
        }

        if (active.id in containerDict && over?.id) {
          setContainerKeys((prevContainerKeys) => {
            const activeIndex = prevContainerKeys.indexOf(active.id as string);
            const overIndex = prevContainerKeys.indexOf(over.id as string);

            return arrayMove(prevContainerKeys, activeIndex, overIndex);
          });
        }

        const activeContainer = findContainer(active.id);

        if (!activeContainer) {
          setActiveId(null);
          return;
        }

        const overId = over?.id;

        if (overId == null) {
          setActiveId(null);
          return;
        }

        const overContainer = findContainer(overId);

        if (overContainer) {
          const activeIndex = containerDict[activeContainer].jobs.findIndex(
            (j) => j.reference === active.id
          );
          const overIndex = containerDict[overContainer].jobs.findIndex(
            (j) => j.reference === overId
          );

          if (activeIndex !== overIndex) {
            const updatedJobs = arrayMove(
              containerDict[overContainer].jobs,
              activeIndex,
              overIndex
            );

            const jobIndexes = updatedJobs.reduce(
              (acc: { [key: string]: number }, job) => {
                acc[job.reference] = updatedJobs.indexOf(job);
                return acc;
              },
              {}
            );

            var arrangeJobsModel = {
              jobListId: overContainer,
              jobIndexes,
            };

            // TODO: fix this
            // await clientApi.put("/JobList/ArrangeJobs", arrangeJobsModel);

            setContainerDict((prevContainerDict) => {
              return {
                ...prevContainerDict,
                [overContainer]: {
                  ...prevContainerDict[overContainer],
                  jobs: updatedJobs,
                },
              };
            });
          }
        }

        setActiveId(null);
      }}
    >
      <div className="flex min-h-[calc(100vh-4rem)] w-screen flex-row overflow-x-scroll border border-x-0 border-t-0 border-r-0 border-gray-200 bg-[#f5f5f4] md:w-[calc(100vw-250px)]">
        <SortableContext
          items={[...containerKeys]}
          strategy={horizontalListSortingStrategy}
        >
          {containerKeys.map((containerId) => (
            <DroppableContainer
              key={containerId}
              id={containerId}
              boardReference={board.reference}
              jobList={containerDict[containerId]}
              items={containerDict[containerId].jobs.map(
                (job) => job.reference
              )}
              onRemove={() => handleRemoveContainer(containerId)}
            >
              <SortableContext
                items={containerDict[containerId].jobs.map((j) => j.reference)}
                strategy={verticalListSortingStrategy}
              >
                {containerDict[containerId].jobs.map((job, index) => {
                  return (
                    <SortableItem
                      disabled={isSortingContainer}
                      key={job.reference}
                      id={job.reference}
                      job={job}
                      index={index}
                    />
                  );
                })}
              </SortableContext>
            </DroppableContainer>
          ))}
        </SortableContext>
      </div>
      {portalElement &&
        createPortal(
          <DragOverlay dropAnimation={dropAnimation}>
            {activeId
              ? containerKeys.includes(activeId)
                ? renderContainerDragOverlay(activeId)
                : renderSortableItemDragOverlay(activeId)
              : null}
          </DragOverlay>,
          portalElement
        )}
    </DndContext>
  );

  function renderSortableItemDragOverlay(id: UniqueIdentifier) {
    const foundJob = containerKeys
      .map((key) => containerDict[key].jobs)
      .flat()
      .find((job) => job.reference === id);

    if (foundJob) {
      return <Item job={foundJob} dragOverlay />;
    }
  }

  function renderContainerDragOverlay(containerId: UniqueIdentifier) {
    return (
      <Container
        boardReference={board.reference}
        jobList={containerDict[containerId]}
        style={{
          height: "100%",
        }}
        shadow
      >
        {containerDict[containerId].jobs.map((job, index) => (
          <Item key={job.reference} job={job} index={index} />
        ))}
      </Container>
    );
  }

  async function handleRemoveContainer(containerID: UniqueIdentifier) {
    // await clientApi.delete(`/joblist/delete/${containerID}`);

    setContainerKeys((prevContainerKeys) =>
      prevContainerKeys.filter((id) => id !== containerID)
    );
  }
}

interface SortableItemProps {
  id: UniqueIdentifier;
  index: number;
  disabled?: boolean;
  job: Job;
}

function SortableItem({ disabled, id, index, job }: SortableItemProps) {
  const {
    setNodeRef,
    setActivatorNodeRef,
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
      handleProps={{ ref: setActivatorNodeRef }}
      index={index}
      transition={transition}
      transform={transform}
      fadeIn={mountedWhileDragging}
      listeners={listeners}
    />
  );
}

function useMountStatus() {
  const [isMounted, setIsMounted] = useState(false);

  useEffect(() => {
    const timeout = setTimeout(() => setIsMounted(true), 500);

    return () => clearTimeout(timeout);
  }, []);

  return isMounted;
}
