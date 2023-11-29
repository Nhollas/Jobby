"use client";

import { Button, Skeleton } from "@/components/ui";
import { Layout } from "lucide-react";
import Link from "next/link";
import { useBoardsQuery } from "@/features/board";

export function BoardsBar() {
  const query = useBoardsQuery();

  if (query.isLoading)
    return (
      <BoardsBarWrapper>
        <LoadingSkeleton />
      </BoardsBarWrapper>
    );
  if (query.isError)
    return (
      <BoardsBarWrapper>
        <p className="text-red-500">Error loading boards</p>
      </BoardsBarWrapper>
    );

  return (
    <BoardsBarWrapper>
      {query.data?.map((board) => (
        <Button
          asChild
          variant="ghost"
          key={board.reference}
          className="w-full justify-start px-4 font-normal"
        >
          <Link
            href={`/track/board/${board.reference}`}
            className="flex flex-row"
          >
            <Layout className="mr-2 h-4 w-4 shrink-0" />
            <span className="truncate">{board.name}</span>
          </Link>
        </Button>
      ))}
    </BoardsBarWrapper>
  );
}

function BoardsBarWrapper({ children }: { children: React.ReactNode }) {
  return (
    <div className="relative h-[300px] overflow-y-auto px-4">{children}</div>
  );
}

function LoadingSkeleton() {
  return Array.from({ length: 4 }).map((_, i) => (
    <Button
      key={i}
      disabled
      variant="ghost"
      className="w-full justify-start px-4 font-normal"
    >
      <Layout className="mr-2 h-4 w-4 shrink-0" />
      <Skeleton className="h-4 w-10/12" />
    </Button>
  ));
}
