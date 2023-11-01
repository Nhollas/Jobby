"use client";

import { useJobQuery } from "@/features/job";

export function JobTitle({ jobRef }: { jobRef: string }) {
  const { data: job } = useJobQuery(jobRef);

  return (
    <div className="flex w-full flex-col items-start justify-start p-4 pb-0">
      <h1 className="text-2xl font-medium">{job?.title}</h1>
      <h2 className="text-gray-500">{job?.company}</h2>
      <p className="text-sm text-gray-500">
        {job ? new Date(job.createdDate).toDateString() : ""}
      </p>
    </div>
  );
}
