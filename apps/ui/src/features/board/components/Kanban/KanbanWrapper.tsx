"use client";
import { Kanban, useBoardQuery } from "@/features/board";

export function KanbanWrapper({ boardRef }: { boardRef: string }) {
  const { data: board } = useBoardQuery(boardRef);

  if (!board) {
    return <p>Loading board...</p>;
  }

  return <Kanban board={board} />;
}
