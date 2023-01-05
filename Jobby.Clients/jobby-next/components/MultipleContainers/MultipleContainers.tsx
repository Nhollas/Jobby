import React, { useCallback, useEffect, useRef, useState } from "react";
import { createPortal } from "react-dom";
import {
  pointerWithin,
  rectIntersection,
  CollisionDetection,
  DndContext,
  DragOverlay,
  DropAnimation,
  getFirstCollision,
  KeyboardSensor,
  MouseSensor,
  TouchSensor,
  useSensors,
  useSensor,
  closestCenter,
  MeasuringStrategy,
  defaultDropAnimationSideEffects,
} from "@dnd-kit/core";
import {
  SortableContext,
  arrayMove,
  verticalListSortingStrategy,
  horizontalListSortingStrategy,
} from "@dnd-kit/sortable";
import { DroppableContainer, SortableItem } from "./components";
import { coordinateGetter } from "./multipleContainersKeyboardCoordinates";
import { Item, Container } from "../../components";
import { Job, JobList } from "../../types";

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
  lists?: JobList[];
}

const PLACEHOLDER_ID = "placeholder";

export function MultipleContainers({ lists }: Props) {
  const [containerDict, setContainerDict] = useState<Record<string, JobList>>(
    lists
      ? lists.reduce((acc, list) => {
          acc[list.id] = list;
          return acc;
        }, {})
      : {}
  );

  const [containerKeys, setContainerKeys] = useState(
    lists ? Object.keys(lists).map((key) => lists[key].id) : []
  );

  const [activeId, setActiveId] = useState<string | null>(null);
  const lastOverId = useRef<string | null>(null);
  const [portalElement, setPortalElement] = useState(null);
  const recentlyMovedToNewContainer = useRef(false);
  const isSortingContainer = activeId
    ? containerKeys.includes(activeId)
    : false;

  useEffect(() => {
    const element = document.createElement("div");
    document.body.appendChild(element);
    setPortalElement(element);
    return () => {
      document.body.removeChild(element);
    };
  }, []);

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
                  containerJobs.some((job) => job.id === container.id)
              ),
            })[0]?.id;
          }
        }

        lastOverId.current = overId as string;

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
  const sensors = useSensors(
    useSensor(MouseSensor),
    useSensor(TouchSensor),
    useSensor(KeyboardSensor, {
      coordinateGetter,
    })
  );

  const findContainer = (id: string) => {
    if (id in containerDict) {
      return id;
    }

    return Object.keys(containerDict).find((key) =>
      containerDict[key].jobs.some((job) => job.id === id)
    );
  };

  function renderSortableItemDragOverlay(jobId: string) {
    let foundJob: Job;
    for (const key of Object.keys(containerDict)) {
      const job = containerDict[key].jobs.find((job) => job.id === jobId);
      if (job) {
        foundJob = job;
        break;
      }
    }

    return <Item job={foundJob} dragOverlay />;
  }

  function renderContainerDragOverlay(containerId: string) {
    return (
      <Container
        list={containerDict[containerId]}
        style={{
          height: "100%",
        }}
        shadow
      >
        {containerDict[containerId].jobs.map((job) => (
          <Item key={job.id} job={job} />
        ))}
      </Container>
    );
  }

  function handleRemove(containerID: string) {
    setContainerKeys((containerKeys) =>
      containerKeys.filter((id) => id !== containerID)
    );
  }

  // TODO this will need to be server side.
  function handleAddColumn() {}

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
        setActiveId(active.id as string);
      }}
      onDragOver={({ active, over }) => {
        const overId = over?.id as string;

        // 1. If we are not moving anything over.
        // 2. Moving a container logic happens in OnDragEnd.
        if (overId == null || active.id in containerDict) {
          return;
        }

        const overContainer = findContainer(overId);
        const activeContainer = findContainer(active.id as string);

        if (!overContainer || !activeContainer) {
          return;
        }

        if (activeContainer !== overContainer) {
          setContainerDict((containerDict) => {
            const activeJobs = containerDict[activeContainer].jobs;
            const overJobs = containerDict[overContainer].jobs;
            const overIndex = overJobs.findIndex((j) => j.id === overId);
            const activeIndex = activeJobs.findIndex((j) => j.id === active.id);
            let newIndex: number;
            if (overId in containerDict[overContainer].jobs) {
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
              ...containerDict,
              [activeContainer]: {
                ...containerDict[activeContainer],
                jobs: containerDict[activeContainer].jobs.filter(
                  (job) => job.id !== active.id
                ),
              },
              [overContainer]: {
                ...containerDict[overContainer],
                jobs: [
                  ...containerDict[overContainer].jobs.slice(0, newIndex),
                  containerDict[activeContainer].jobs[activeIndex],
                  ...containerDict[overContainer].jobs.slice(
                    newIndex,
                    containerDict[overContainer].jobs.length
                  ),
                ],
              },
            };
          });
        }
      }}
      onDragEnd={({ active, over }) => {
        // We are moving a container.
        if (active.id in containerDict && over?.id) {
          setContainerKeys((containerKeys) => {
            const activeIndex = containerKeys.indexOf(active.id);
            const overIndex = containerKeys.indexOf(over.id);

            return arrayMove(containerKeys, activeIndex, overIndex);
          });
        }

        const activeContainer = findContainer(active.id as string);

        if (!activeContainer) {
          setActiveId(null);
          return;
        }

        const overId = over?.id as string;

        if (overId == null) {
          setActiveId(null);
          return;
        }

        // TODO: Will need to be server side.
        if (overId === PLACEHOLDER_ID) {
          return;
        }

        const overContainer = findContainer(overId);

        if (overContainer) {
          const activeIndex = containerDict[activeContainer].jobs.findIndex(
            (j) => j.id === active.id
          );
          const overIndex = containerDict[overContainer].jobs.findIndex(
            (j) => j.id === overId
          );

          if (activeIndex !== overIndex) {
            setContainerDict((containerDict) => ({
              ...containerDict,
              [overContainer]: {
                ...containerDict[overContainer],
                jobs: arrayMove(
                  containerDict[overContainer].jobs,
                  activeIndex,
                  overIndex
                ),
              },
            }));
          }
        }

        setActiveId(null);
      }}
    >
      <div className='flex h-full divide-x'>
        <SortableContext
          items={[...containerKeys, PLACEHOLDER_ID]}
          strategy={horizontalListSortingStrategy}
        >
          {containerKeys.map((containerId) => (
            <DroppableContainer
              key={containerId}
              id={containerId}
              list={containerDict[containerId]}
              items={containerDict[containerId].jobs}
              onRemove={() => handleRemove(containerId)}
            >
              <SortableContext
                items={containerDict[containerId].jobs}
                strategy={verticalListSortingStrategy}
              >
                {containerDict[containerId].jobs.map((job, index) => {
                  return (
                    <SortableItem
                      key={job.id}
                      id={job.id}
                      index={index}
                      disabled={isSortingContainer}
                      job={job}
                    />
                  );
                })}
              </SortableContext>
            </DroppableContainer>
          ))}
          <DroppableContainer
            id={PLACEHOLDER_ID}
            disabled={isSortingContainer}
            items={[]}
            onClick={handleAddColumn}
            placeholder
          >
            + Add column
          </DroppableContainer>
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
}
