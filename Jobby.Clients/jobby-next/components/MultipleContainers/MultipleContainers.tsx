import { useCallback, useEffect, useRef, useState } from "react";
import { createPortal, unstable_batchedUpdates } from "react-dom";
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
import { client } from "../../client";
import { CreateJobListRequest } from "../../types/requests/JobList";

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
  boardId: string;
}

const PLACEHOLDER_ID = "placeholder";

export function MultipleContainers({ lists, boardId }: Props) {
  const [containerDict, setContainerDict] = useState<Record<string, JobList>>(
    lists
      ? lists.reduce((acc, list) => {
          acc[list.id] = list;
          return acc;
        }, {})
      : {}
  );

  const [containerKeys, setContainerKeys] = useState<string[]>(
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

  async function handleRemove(containerId: string) {
    setContainerKeys((containerKeys) =>
      containerKeys.filter((id) => id !== containerId)
    );

    await client.delete(`/JobList/Delete/${containerId}`);
  }

  // TODO this will need to be server side.
  function handleAddColumn() {}

  return (
    <div className='h-full overflow-x-scroll'>
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
              const activeIndex = activeJobs.findIndex(
                (j) => j.id === active.id
              );

              if (overId in containerDict[overContainer].jobs) {
                newIndex = overJobs.length + 1;
              } else {
                const middleTranslated =
                  (active.rect.current.translated.top +
                    active.rect.current.translated.bottom) /
                  2;

                const middleOver = over.rect.top + over.rect.height;

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

            client.put("/Job/Move", model);
          }
        }}
        onDragEnd={async ({ active, over }) => {
          // We are moving a container.
          if (active.id in containerDict && over?.id) {
            setContainerKeys((containerKeys) => {
              const activeIndex = containerKeys.indexOf(active.id as string);
              const overIndex = containerKeys.indexOf(over.id as string);

              const updatedJobLists = arrayMove(
                containerKeys,
                activeIndex,
                overIndex
              );

              const jobListIndexes = updatedJobLists.reduce((acc, list) => {
                acc[list] = updatedJobLists.indexOf(list);
                return acc;
              }, {});

              var model = {
                boardId,
                jobListIndexes,
              };

              client.put("/Board/ArrangeJobLists", model);

              return updatedJobLists;
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

          if (overId === PLACEHOLDER_ID) {
            const tempId = crypto.randomUUID();
            setContainerDict((prevDict) => {
              const jobToMove = Object.values(prevDict)
                .flatMap((list) => list.jobs)
                .find((j) => j.id === active.id);

              const tempJobList: JobList = {
                id: tempId,
                createdDate: new Date().toDateString(),
                name: "Loading...",
                index: containerKeys.length,
                lastUpdated: new Date().toDateString(),
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

            // TODO: Display modal and then send post request to create the Real JobList.

            // const request: CreateJobListRequest = {
            //   boardId,
            //   name: "Test",
            //   index: containerKeys.length + 1,
            //   initJobId: active.id as string,
            // };

            // const createdJobList = await client.post<
            //   CreateJobListRequest,
            //   JobList
            // >("/JobList/Create", request);

            // setContainerKeys((prevKeys) => [...prevKeys, createdJobList.id]);
            // setContainerDict((prevDict) => {
            //   const jobToMove = Object.values(prevDict)
            //     .flatMap((list) => list.jobs)
            //     .find((j) => j.id === active.id);

            //   return {
            //     ...prevDict,
            //     [jobToMove.jobListId]: {
            //       ...prevDict[jobToMove.jobListId],
            //       jobs: [
            //         ...prevDict[jobToMove.jobListId].jobs.filter(
            //           (j) => j.id !== jobToMove.id
            //         ),
            //       ],
            //     },
            //     [createdJobList.id]: {
            //       ...createdJobList,
            //       jobs: [{ ...jobToMove, jobListId: createdJobList.id }],
            //     },
            //   };
            // });
            // setActiveId(null);
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
              setContainerDict((containerDict) => {
                {
                  const updatedJobs = arrayMove(
                    containerDict[overContainer].jobs,
                    activeIndex,
                    overIndex
                  );

                  const jobIndexes = updatedJobs.reduce((acc, job) => {
                    acc[job.id] = updatedJobs.indexOf(job);
                    return acc;
                  }, {});

                  var model = {
                    jobListId: overContainer,
                    jobIndexes,
                  };

                  client.put("/JobList/ArrangeJobs", model);

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
              setContainerDict((containerDict) => {
                const jobIndexes = containerDict[overContainer].jobs.reduce(
                  (acc, job) => {
                    acc[job.id] =
                      containerDict[overContainer].jobs.indexOf(job);
                    return acc;
                  },
                  {}
                );

                var model = {
                  jobListId: overContainer,
                  jobIndexes,
                };

                client.put("/JobList/ArrangeJobs", model);

                return containerDict;
              });
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
            {containerKeys.map((containerId) => {
              return (
                <DroppableContainer
                  key={containerId}
                  id={containerId}
                  list={containerDict[containerId]}
                  items={containerDict[containerId].jobs}
                  onRemove={
                    containerDict[containerId].jobs.length === 0
                      ? () => handleRemove(containerId)
                      : null
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
    </div>
  );
}
