"use client";

import { ActivityFilter, CreateActivityModal } from "@/features/activity";

export default function Page({
  searchParams,
}: {
  searchParams: { boardRef: string; filter: ActivityFilter; jobRef?: string };
}) {
  const { filter, jobRef, boardRef } = searchParams;

  console.log("searchParams", searchParams);

  return (
    <CreateActivityModal boardRef={boardRef} jobRef={jobRef} filter={filter} />
  );
}
