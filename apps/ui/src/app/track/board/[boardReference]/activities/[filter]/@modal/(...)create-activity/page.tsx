"use client";

import { ActivityFilter, CreateActivityModal } from "@/features/activity";

export default function Page({
  searchParams,
}: {
  searchParams: {
    boardReference: string;
    filter: ActivityFilter;
    jobRef?: string;
  };
}) {
  const { filter, jobRef, boardReference } = searchParams;

  return (
    <CreateActivityModal
      boardRef={boardReference}
      filter={filter}
      jobRef={jobRef}
    />
  );
}
