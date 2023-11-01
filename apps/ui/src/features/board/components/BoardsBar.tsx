"use client";

import { Button } from "@/components/ui";
import { Layout } from "lucide-react";
import Link from "next/link";
import { useBoardsQuery } from "@/features/board";

export function BoardsBar() {
  const { data: boards } = useBoardsQuery();
  return (
    <div
      dir="ltr"
      className="relative h-[300px] overflow-y-scroll overscroll-contain px-4"
    >
      {boards?.map((board) => (
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
    </div>
  );
}
