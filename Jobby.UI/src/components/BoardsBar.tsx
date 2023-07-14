"use client";

import { Button } from "@/components/ui/button";
import { GetBoardsResponse } from "@/contracts/queries/GetBoards";
import { useBoardsQuery } from "@/hooks/useBoardsData";
import { Layout } from "lucide-react";
import Link from "next/link";

export const BoardsBar = ({
  initialBoards,
}: {
  initialBoards?: GetBoardsResponse;
}) => {
  const { data: boards } = useBoardsQuery(initialBoards);
  return (
    <div
      dir="ltr"
      className="relative h-[300px] overflow-y-scroll overscroll-contain px-4"
    >
      {boards?.map((board) => (
        <Button
          asChild
          variant="ghost"
          key={board.id}
          className="w-full justify-start px-4 font-normal"
        >
          <Link href={`/board/${board.id}`} className="flex flex-row">
            <Layout className="mr-2 h-4 w-4 shrink-0" />
            <span className="truncate">{board.name}</span>
          </Link>
        </Button>
      ))}
    </div>
  );
};
