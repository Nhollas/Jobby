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
import { Input } from "@/components/ui/input";
import { Board } from "@/types";
import { useBoardsQuery } from "@/hooks/useBoardsData";

export function Boards({ boards: initialBoards }: { boards: Board[] }) {
  const { data: boards } = useBoardsQuery(initialBoards);

  return (
    <div className="flex flex-col gap-y-6 p-4 lg:px-8">
      <div className="flex flex-col gap-y-2">
        <h1 className="text-2xl font-medium">Boards</h1>
        <p className="text-sm text-gray-500">View and manage boards</p>
      </div>
      <div className="flex flex-row gap-x-4">
        <Input
          type="email"
          placeholder="Search.."
          className="w-full max-w-xs"
        />
        <Button asChild>
          <Link href="/create-board" className="w-max">
            Create Board
          </Link>
        </Button>
      </div>
      <div className="grid grid-cols-[repeat(auto-fill,minmax(250px,1fr))] gap-8">
        {boards?.map((board) => (
          <Card key={board.id}>
            <CardHeader>
              <CardTitle>{board.name}</CardTitle>
              <CardDescription>
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
}
