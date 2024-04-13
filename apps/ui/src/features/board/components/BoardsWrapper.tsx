"use client";

import { Skeleton } from "@/components/ui";
import { Boards, useBoardsQuery } from "@/features/board";

export function BoardsWrapper() {
  const query = useBoardsQuery();

  if (query.isError)
    return <p className="text-red-500">Error loading boards.</p>;

  if (query.isLoading) return <LoadingSkeleton />;

  return <Boards boards={query.data ?? []} />;
}

function LoadingSkeleton() {
  return (
    <div className="w-full max-w-5xl">
      <div className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
        {Array.from({ length: 4 }).map((_, i) => (
          <Skeleton key={i} className="h-44 w-full animate-pulse rounded-md" />
        ))}
      </div>
    </div>
  );
}
