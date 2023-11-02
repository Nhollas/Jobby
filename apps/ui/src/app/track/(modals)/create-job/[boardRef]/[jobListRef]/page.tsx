"use client";

import { useBoardsQuery } from "@/features/board";
import { CreateJobModal } from "@/features/job";

export default function Page({
  params: { boardRef, jobListRef },
}: {
  params: { boardRef: string; jobListRef: string };
}) {
  const { data: boards } = useBoardsQuery();

  if (!boards) {
    return <p>Loading boards...</p>;
  }

  return (
    <CreateJobModal
      boards={boards}
      boardRef={boardRef}
      jobListRef={jobListRef}
    />
  );
}
