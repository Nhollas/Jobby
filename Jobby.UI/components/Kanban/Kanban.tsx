"use client";

import { useCallback, useEffect, useRef, useState } from "react";
import { createPortal } from "react-dom";
import {
  pointerWithin,
  rectIntersection,
  CollisionDetection,
  DndContext,
  DragOverlay,
  getFirstCollision,
  KeyboardSensor,
  MouseSensor,
  TouchSensor,
  useSensors,
  useSensor,
  closestCenter,
  MeasuringStrategy,
  UniqueIdentifier,
} from "@dnd-kit/core";
import {
  SortableContext,
  verticalListSortingStrategy,
  arrayMove,
  horizontalListSortingStrategy,
} from "@dnd-kit/sortable";
import { DroppableContainer, SortableItem } from "./components";
import { Item } from "@/components/Item";
import { Container } from "@/components/Container";
import { dropAnimation } from "./kanbanUtils";
import { coordinateGetter } from "./kanbanKeyboardCoordinates";
import { JobList } from "@/types";
import { clientApi } from "@/lib/clients/clientApi";
import { useAuth } from "@clerk/nextjs";

interface Props {
  lists: JobList[];
  boardId: string;
}

export const TRASH_ID = "void";
const PLACEHOLDER_ID = "placeholder";

export function Kanban({ lists, boardId }: Props) {
  const { getToken } = useAuth();

  const [containerDict, setContainerDict] = useState<Record<string, JobList>>(
    lists.reduce((acc: Record<string, JobList>, list) => {
      acc[list.id] = list;
      return acc;
    }, {})
  );

  const [containerKeys, setContainerKeys] = useState<string[]>(
    Object.keys(containerDict).map((key) => containerDict[key].id)
  );

  const [activeId, setActiveId] = useState<string | null>(null);
  const [moveJob, setMoveJob] = useState<{
    jobId: UniqueIdentifier;
    targetJobListId: string;
  } | null>(null);
  const lastOverId = useRef<string | null>(null);
  const [portalElement, setPortalElement] = useState<HTMLDivElement | null>(
    null
  );
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

  useEffect(() => {
    requestAnimationFrame(() => {
      recentlyMovedToNewContainer.current = false;
    });
  }, [containerDict]);

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
    useSensor(MouseSensor, {
      activationConstraint: {
        delay: 250,
        tolerance: 5,
      },
    }),
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
    const foundJob = lists
      .flatMap((list) => list.jobs)
      .find((job) => job.id === jobId);

    if (foundJob) {
      return <Item job={foundJob} dragOverlay />;
    }
  }

  function renderContainerDragOverlay(containerId: string) {
    return (
      <Container list={containerDict[containerId]} boardId={boardId}>
        {containerDict[containerId].jobs.map((job) => (
          <Item key={job.id} job={job} />
        ))}
      </Container>
    );
  }

  async function handleRemove(containerId: string) {
    const response = await clientApi.delete(`/JobList/Delete/${containerId}`);

    if (response?.status == 204) {
      setContainerKeys((containerKeys) =>
        containerKeys.filter((id) => id !== containerId)
      );
    }
  }

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
      onDragOver={async ({ active, over }) => {
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
        let newIndex: number;

        if (activeContainer !== overContainer) {
          setContainerDict((containerDict) => {
            const activeJobs = containerDict[activeContainer].jobs;
            const overJobs = containerDict[overContainer].jobs;
            const overIndex = overJobs.findIndex((j) => j.id === overId);
            const activeIndex = activeJobs.findIndex((j) => j.id === active.id);

            if (overId in containerDict[overContainer].jobs) {
              newIndex = overJobs.length + 1;
            } else {
              const middleTranslated =
                (active.rect.current.translated!.top +
                  active.rect.current.translated!.bottom) /
                2;

              const middleOver = over!.rect.top + over!.rect.height;

              const isBelowOverItem = middleTranslated >= middleOver;

              const modifier = isBelowOverItem ? 1 : 0;

              if (overJobs.length === 0) {
                newIndex = 0;
              } else {
                newIndex =
                  overIndex >= 0 ? overIndex + modifier : overJobs.length + 1;
              }
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
                ].map((job, i) => ({
                  ...job,
                  index: i,
                  jobListId: overContainer,
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
          await clientApi.put("/Job/Move", moveJob, {
            headers: {
              Authorization: `Bearer ${await getToken()}`,
            },
          });
        }

        // We are moving a container.
        if (active.id in containerDict && over?.id) {
          const activeIndex = containerKeys.indexOf(active.id as string);
          const overIndex = containerKeys.indexOf(over.id as string);

          const updatedJobLists = arrayMove(
            containerKeys,
            activeIndex,
            overIndex
          );

          const jobListIndexes = updatedJobLists.reduce(
            (acc: { [key: string]: number }, list) => {
              acc[list] = updatedJobLists.indexOf(list);
              return acc;
            },
            {}
          );

          var model = {
            boardId,
            jobListIndexes,
          };

          await clientApi.put("/Board/ArrangeJobLists", model, {
            headers: {
              Authorization: `Bearer ${await getToken()}`,
            },
          });

          setContainerKeys(updatedJobLists);

          return;
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

        if (overId === PLACEHOLDER_ID) {
          const tempId = crypto.randomUUID();
          setContainerDict((prevDict) => {
            const jobToMove = Object.values(prevDict)
              .flatMap((list) => list.jobs)
              .find((j) => j.id === active.id);

            if (!jobToMove) {
              return prevDict;
            }

            const tempJobList: JobList = {
              id: tempId,
              createdDate: new Date(),
              boardId: boardId,
              name: "Loading...",
              lastUpdated: new Date(),
              jobs: [{ ...jobToMove, jobListId: tempId }],
            };

            return {
              ...prevDict,
              [jobToMove.jobListId]: {
                ...prevDict[jobToMove.jobListId],
                jobs: [
                  ...prevDict[jobToMove.jobListId].jobs.filter(
                    (j) => j.id !== jobToMove.id
                  ),
                ],
              },
              [tempJobList.id]: tempJobList,
            };
          });

          setContainerKeys((prevKeys) => [...prevKeys, tempId]);

          // TODO set up cloned items, incase we cancel the creation of the new Joblist.

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
            const updatedJobs = arrayMove(
              containerDict[overContainer].jobs,
              activeIndex,
              overIndex
            );

            const jobIndexes = updatedJobs.reduce(
              (acc: { [key: string]: number }, job) => {
                acc[job.id] = updatedJobs.indexOf(job);
                return acc;
              },
              {}
            );

            var arrangeJobsModel = {
              jobListId: overContainer,
              jobIndexes,
            };

            await clientApi.put("/JobList/ArrangeJobs", arrangeJobsModel, {
              headers: {
                Authorization: `Bearer ${await getToken()}`,
              },
            });

            setContainerDict((containerDict) => {
              {
                return {
                  ...containerDict,
                  [overContainer]: {
                    ...containerDict[overContainer],
                    jobs: updatedJobs,
                  },
                };
              }
            });
          } else {
            const jobIndexes = containerDict[overContainer].jobs.reduce(
              (acc: { [key: string]: number }, job) => {
                acc[job.id] = containerDict[overContainer].jobs.indexOf(job);
                return acc;
              },
              {}
            );

            var sussyModel = {
              jobListId: overContainer,
              jobIndexes,
            };

            await clientApi.put("/JobList/ArrangeJobs", sussyModel, {
              headers: {
                Authorization: `Bearer ${await getToken()}`,
              },
            });
          }
        }

        setActiveId(null);
      }}
    >
      <div className="relative mt-16 flex h-full w-[calc(100vw-250px)] flex-row divide-x overflow-x-scroll border border-t-0 border-l-0 border-gray-300 bg-[#f5f5f4]">
        <SortableContext
          items={[...containerKeys, PLACEHOLDER_ID]}
          strategy={horizontalListSortingStrategy}
        >
          {containerKeys.map((containerId) => {
            return (
              <DroppableContainer
                key={containerId}
                id={containerId}
                list={containerDict[containerId]}
                items={containerDict[containerId].jobs}
                boardId={boardId}
                onRemove={
                  containerDict[containerId].jobs.length === 0
                    ? () => handleRemove(containerId)
                    : undefined
                }
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
            );
          })}
          <DroppableContainer
            id={PLACEHOLDER_ID}
            disabled={isSortingContainer}
            items={[]}
            onClick={() => {}}
            placeholder
            boardId={boardId}
          >
            <p>+ Add column</p>
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
