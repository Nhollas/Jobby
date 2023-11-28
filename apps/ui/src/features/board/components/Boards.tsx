"use client";

import Link from "next/link";
import { Plus, Trash2 } from "lucide-react";
import {
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
  Button,
} from "@/components/ui";
import { Board } from "@/types";

export const Boards = ({ boards }: { boards: Board[] }) => {
  console.log(boards);

  return (
    <div className="flex flex-col gap-y-6 p-6">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Boards</h1>
        <p className="text-sm text-gray-500">View and manage boards</p>
      </div>
      <Button asChild size="sm" variant="outline" className="w-max">
        <Link href={`/track/create-board`}>Create Board</Link>
      </Button>
      <div className="grid grid-cols-[repeat(auto-fill,minmax(200px,1fr))] gap-8">
        {boards.map((board) => (
          <Card
            key={board.reference}
            className="card"
            data-board-ref={board.reference}
          >
            <CardHeader>
              <CardTitle className="card-title truncate leading-tight">
                {board.name}
              </CardTitle>
              <CardDescription className="card-description">
                {new Date(board.createdDate).toDateString()}
              </CardDescription>
            </CardHeader>
            <CardFooter>
              <div className="flex w-full flex-row gap-x-3">
                <Button asChild size="sm" variant="outline">
                  <Link href={`/track/delete-board/${board.reference}`}>
                    <Trash2 size={18} />
                  </Link>
                </Button>
                <Button
                  asChild
                  variant="outline"
                  className="flex w-full"
                  size="sm"
                >
                  <Link href={`/track/board/${board.reference}`}>View</Link>
                </Button>
              </div>
            </CardFooter>
          </Card>
        ))}
      </div>
    </div>
  );
};
