"use client";

import Link from "next/link";
import { Trash2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { useBoardsQuery } from "@/hooks/useBoardsData";
import { GetBoardsResponse } from "@/contracts/queries/GetBoards";

export const Boards = ({
  initialBoards,
}: {
  initialBoards?: GetBoardsResponse;
}) => {
  const { data: boards } = useBoardsQuery(initialBoards);

  return (
    <div className="flex flex-col gap-y-6 p-6">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-lg font-semibold tracking-tight">Boards</h1>
        <p className="text-sm text-gray-500">View and manage boards</p>
      </div>
      <Button asChild>
        <Link href="/create-board" className="w-max">
          Create Board
        </Link>
      </Button>
      <div className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
        {boards?.map((board) => (
          <Card key={board.id} className="card" data-board-id={board.id}>
            <CardHeader>
              <CardTitle className="card-title">{board.name}</CardTitle>
              <CardDescription className="card-description">
                {new Date(board.createdDate).toDateString()}
              </CardDescription>
            </CardHeader>
            <CardFooter>
              <div className="flex w-full flex-row gap-x-3">
                <Button asChild size="sm" variant="outline">
                  <Link href={`/delete-board/${board.id}`}>
                    <Trash2 size={18} />
                  </Link>
                </Button>
                <Button
                  asChild
                  variant="outline"
                  className="flex w-full"
                  size="sm"
                >
                  <Link href={`/board/${board.id}`}>View</Link>
                </Button>
              </div>
            </CardFooter>
          </Card>
        ))}
      </div>
    </div>
  );
};
