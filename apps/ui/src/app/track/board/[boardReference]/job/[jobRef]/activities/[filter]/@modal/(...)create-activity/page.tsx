"use client";

import { ActivityFilter, CreateActivityModal } from "@/features/activity";

export default function Page({
  searchParams,
}: {
  searchParams: {
    boardReference: string;
    filter: ActivityFilter;
    jobRef: string;
  };
}) {
  const { filter, boardReference, jobRef } = searchParams;

  return (
    <CreateActivityModal
      boardRef={boardReference}
      jobRef={jobRef}
      filter={filter}
    />
  );
}
