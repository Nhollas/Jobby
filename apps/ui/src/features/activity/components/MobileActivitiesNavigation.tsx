"use client";

import { Filter } from "lucide-react";
import { useState } from "react";
import {
  Sheet,
  SheetContent,
  SheetTrigger,
  Button,
  Separator,
} from "@/components/ui";
import { ActivityItem } from "./ActivitiesNavigation";

export function MobileActivitiesNavigation({
  filter,
  boardRef,
  jobRef,
}: {
  filter: string;
  boardRef: string;
  jobRef?: string;
}) {
  const [open, setOpen] = useState(false);
  return (
    <Sheet open={open} onOpenChange={setOpen}>
      <div className="px-4 pt-4 md:hidden">
        <SheetTrigger asChild>
          <Button
            size="sm"
            variant="outline"
            className="
            flex h-10 w-max items-center gap-x-2 rounded-md bg-white px-4 py-2 text-sm font-medium text-primary"
          >
            <Filter className="h-5 w-5" />
            Filter
          </Button>
        </SheetTrigger>
      </div>
      <MobileNavigationContent
        filter={filter}
        boardRef={boardRef}
        jobRef={jobRef}
      />
    </Sheet>
  );
}

function MobileNavigationContent({
  filter,
  boardRef,
  jobRef,
}: {
  filter: string;
  boardRef: string;
  jobRef?: string;
}) {
  const jobPath = jobRef ? `/job/${jobRef}` : "";

  return (
    <SheetContent side="left" className="w-[250px] p-0 py-6 md:hidden">
      <div className="w-[250px] pt-4">
        <div className="fixed flex w-[250px] flex-col gap-y-2">
          <div className="flex flex-col gap-y-1 p-2">
            <ActivityItem
              title="All"
              href={`/track/board/${boardRef}${jobPath}/activities/all`}
              active={filter === "all"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Due Today"
              href={`/track/board/${boardRef}${jobPath}/activities/due-today`}
              active={filter === "due-today"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Past Due"
              href={`/track/board/${boardRef}${jobPath}/activities/past-due`}
              active={filter === "past-due"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Completed"
              href={`/track/board/${boardRef}${jobPath}/activities/completed`}
              active={filter === "completed"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Pending"
              href={`/track/board/${boardRef}${jobPath}/activities/pending`}
              active={filter === "pending"}
              layoutId={jobRef ? jobRef : boardRef}
            />
          </div>
          <Separator className="-z-20" />
          <div className="flex flex-col gap-y-1 p-2">
            <ActivityItem
              title="Applications"
              href={`/track/board/${boardRef}${jobPath}/activities/applications`}
              active={filter === "applications"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Interviews"
              href={`/track/board/${boardRef}${jobPath}/activities/interviews`}
              active={filter === "interviews"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Offers"
              href={`/track/board/${boardRef}${jobPath}/activities/offers`}
              active={filter === "offers"}
              layoutId={jobRef ? jobRef : boardRef}
            />
            <ActivityItem
              title="Networking"
              href={`/track/board/${boardRef}${jobPath}/activities/networking`}
              active={filter === "networking"}
              layoutId={jobRef ? jobRef : boardRef}
            />
          </div>
        </div>
      </div>
    </SheetContent>
  );
}
