"use client";

import { Activity } from "types";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import Link from "next/link";

type Props = {
  activities: Activity[];
  boardId: string;
  jobId?: string;
};

const createUrl = (boardId?: string, jobId?: string) => {
  const params = new URLSearchParams();

  if (boardId) params.set("boardId", boardId);
  if (jobId) params.set("jobId", jobId);

  return `/create-activity${params.toString() ? `?${params.toString()}` : ""}`;
};

export const Activities = ({ activities, boardId, jobId }: Props) => {
  return (
    <div className="flex flex-col gap-y-6 overscroll-contain border-gray-300 p-4 lg:px-8">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Activities</h1>
        <p className="text-sm text-gray-500">View and manage activities</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input type="text" placeholder="Search.." className="w-full max-w-xs" />
        <Button asChild>
          <Link
            href={createUrl(boardId, jobId)}
            className="w-max whitespace-nowrap"
          >
            Create Activity
          </Link>
        </Button>
      </div>
    </div>
  );
};
