"use client";
import { ApiErrorMessage } from "@/components";
import { Kanban, useBoardQuery } from "@/features/board";

export function KanbanWrapper({ boardRef }: { boardRef: string }) {
  const query = useBoardQuery(boardRef);

  if (query.isError)
    return (
      <ApiErrorMessage
        error={{
          status: 500,
          message: "Error...",
        }}
      />
    );
  if (query.isLoading) return <p>Loading...</p>;

  const board = query.data;

  return <Kanban board={board} />;
}
